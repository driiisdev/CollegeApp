using CollegeApp.MyLogging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Minute)
    .CreateLogger();

builder.Host.UseSerilog();
//builder.Logging.AddSerilog();

// Add services to the container.
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();

builder.Services.AddScoped<IMyLogger, LogToFile>();
builder.Services.AddSingleton<IMyLogger, LogToDB>();
builder.Services.AddTransient<IMyLogger, LogToServerMemory>();
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
