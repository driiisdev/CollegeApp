//using CollegeApp.MyLogging;
//using Serilog;
using log4net.Config;
using log4net;
using CollegeApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddLog4Net();

// Configure logging to use log4net
builder.Logging.AddLog4Net();

// Initialize log4net from log4net.config
var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

#region Serilog Settings
//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Debug()
//    .WriteTo.Console()
//    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Minute)
//    .CreateLogger();

// use  this line to override the built-in loggers 
//builder.Host.UseSerilog();

// use serilog along with built-in loggers
//builder.Logging.AddSerilog();
#endregion

builder.Services.AddDbContext<CollegeDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDBConnection"));
});

// Add services to the container.
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();

//builder.Services.AddScoped<IMyLogger, LogToFile>();
//builder.Services.AddSingleton<IMyLogger, LogToDB>();
//builder.Services.AddTransient<IMyLogger, LogToServerMemory>();

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

app.UseAuthentication();

app.MapControllers();

app.Run();
