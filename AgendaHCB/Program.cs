using AgendaHCB.Services;
using BussinessLogic.Implementation;
using BussinessLogic.Interfaces;
using CommonMethods;
using DataAccess.Implementation;
using DataAccess.Interfaces;

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

app.Run();
