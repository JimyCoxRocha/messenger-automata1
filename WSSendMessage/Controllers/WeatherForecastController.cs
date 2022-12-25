using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using WSSendMessage.Settings;

namespace WSSendMessage.Controllers
{
    [ApiController]
    [Route("Message")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly SendGridConfig _sendGrid;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<SendGridConfig> sendGrid)
        {
            _logger = logger;
            _sendGrid = sendGrid.Value;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost(Name = "Send")]
        public void POST([FromBody] SendPhoneMessageRequest data)
        {
            if (data?.CellPhone == null || data?.ClientName == null || data?.UrlWS == null)
                return;

            var accountSid = _sendGrid.AccountSid;
            var authToken = _sendGrid.AuthToken;
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber("+593979214297"));
            messageOptions.MessagingServiceSid = _sendGrid.MessagingServiceSid;
            messageOptions.Body = $"{data.CellPhone} - {data.ClientName} - {data.UrlWS}";

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
            
        }
    }
}