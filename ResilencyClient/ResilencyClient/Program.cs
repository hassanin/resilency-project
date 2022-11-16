using static ResilencyClient.Startup.Utils;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
//  https://github.com/App-vNext/Polly/wiki/Polly-and-HttpClientFactory
//Explains how to do retry
// https://stackoverflow.com/questions/62084405/polly-retry-with-different-url
builder.Host.ConfigureAppConfiguration(ConfigureAppConfiguration)
            .ConfigureServices(ConfigureServices);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
