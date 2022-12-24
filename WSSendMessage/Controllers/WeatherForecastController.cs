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
    [Route("[controller]")]
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

        [HttpPost(Name = "GetWeatherForecast")]
        public void POST(string url)
        {
            var accountSid = _sendGrid.AccountSid;
            var authToken = _sendGrid.AuthToken;
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber("+593962746928"));
            messageOptions.MessagingServiceSid = _sendGrid.MessagingServiceSid;
            messageOptions.Body = url;

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
            
        }
    }
}