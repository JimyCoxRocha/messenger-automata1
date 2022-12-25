using WSSendMessage.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<SendGridConfig>(x =>
{
    x.AccountSid = builder.Configuration["MessengerAutomataAccountSid"];
    x.AuthToken = builder.Configuration["MessengerAutomataAuthToken"];
    x.MessagingServiceSid = builder.Configuration["MessengerAutomataMessagingServiceSid"];
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
