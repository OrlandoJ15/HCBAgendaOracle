using AgendaHCB.Services;
using BusinessLogic.Implementation;
using BusinessLogic.Interfaces;
using BussinessLogic.Implementation;
using BussinessLogic.Interfaces;
using CommonMethods;
using DataAccess.Implementation;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using NLog;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// -------------------- CONFIGURACI�N NLOG --------------------
builder.Logging.ClearProviders();
builder.Host.UseNLog();

// -------------------- SERVICIOS --------------------

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Servicios propios
builder.Services.AddSingleton<OracleService>();
builder.Services.AddSingleton<AsyncExceptions>();
builder.Services.AddScoped<Exceptions>();

// DI para capa de datos y l�gica
builder.Services.AddScoped<IAgendaDA, AgendaDA>();
builder.Services.AddScoped<IAgendaBL, AgendaBL>();
builder.Services.AddScoped<ICitaDA, CitaDA>();
builder.Services.AddScoped<ICitaBL, CitaBL>();
builder.Services.AddScoped<IEspecialidadesDA, EspecialidadesDA>();

var app = builder.Build();

// -------------------- MIDDLEWARE --------------------

// Manejo global de errores (antes de MapControllers)
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Error(contextFeature.Error, "Error no manejado en la aplicaci�n");

            await context.Response.WriteAsJsonAsync(new
            {
                error = "Error interno del servidor",
                detail = contextFeature.Error.Message
            });
        }
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
