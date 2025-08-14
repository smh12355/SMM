using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;

using SMM.CompositionRoot;

var builder = WebApplication.CreateBuilder(args);

// Чистим провайдеры и добавляем консоль
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Debug);

// Фильтры для подробных логов EF и ASP.NET Core
builder.Logging.AddFilter("Microsoft.AspNetCore", LogLevel.Information);
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Debug);
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Connection", LogLevel.Debug);
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Query", LogLevel.Debug);

// Добавляем HTTP логирование
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
});

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestPath
                            | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponseStatusCode;
});
// Add services to the container.
builder.Services.AddSmmServices(builder.Configuration); 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Глобальный обработчик ошибок с логированием
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;

        logger.LogError(exception, "Произошла необработанная ошибка");

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Произошла ошибка");
    });
});

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
