using WSSendMessage.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.Configure<SendGridConfig>(x =>
{
    x.AccountSid = builder.Configuration["MessengerAutomata:AccountSid"];
    x.AuthToken = builder.Configuration["MessengerAutomata:AuthToken"];
    x.MessagingServiceSid = builder.Configuration["MessengerAutomata:MessagingServiceSid"];
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
