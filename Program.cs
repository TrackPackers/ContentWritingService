using System;
using ContentWriterService.Context;
using ContentWriterService.Messaging;
using ContentWriterService.Services;
using ContentWriterService.Services.Interfaces;
using ContentWriterService.Messaging.Interfaces;
using ContentWriterService.Context.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var mongoUri = builder.Configuration.GetValue<string>("MONGODB_URI");

// Add services to the container.
builder.Services.AddSingleton<IDbContentContext>(new DbContentContext(mongoUri, "ContentWriterDB"));
builder.Services.AddScoped<IKafkaController, KafkaController>();

builder.Services.AddSingleton<KafkaController>();
builder.Services.AddScoped<IContentService, ContentService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
