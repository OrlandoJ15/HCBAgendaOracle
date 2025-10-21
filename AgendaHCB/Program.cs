using AgendaHCB.Services;
using BussinessLogic.Implementation;
using BussinessLogic.Interfaces;
using CommonMethods;
using DataAccess.Implementation;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Servicios propios
builder.Services.AddSingleton<OracleService>();

// Inyección de AgendaAD con IConfiguration
builder.Services.AddTransient<IAgendaAD, AgendaAD>();
builder.Services.AddScoped<IAgendaAD, AgendaAD>();
builder.Services.AddScoped<IAgendaLN, AgendaLN>();
builder.Services.AddScoped<IEspecialidadesDA, EspecialidadesDA>();
builder.Services.AddScoped<Exceptions>();


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

//Manejo de errores
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
            logger.Error(contextFeature.Error);

            await context.Response.WriteAsJsonAsync(new
            {
                error = "Error interno del servidor",
                detail = contextFeature.Error.Message
            });
        }
    });
});

app.Run();
