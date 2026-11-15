using SistemadeMensajeria.Application.Contract;
using SistemadeMensajeria.Application.Services;
using Microsoft.EntityFrameworkCore;
using SistemadeMensajeria.Domain.Interfaces.Repositorios;
using SistemadeMensajeria.Infrastructure.AccesoDatos.Contexto;
using SistemadeMensajeria.Infrastructure.AccesoDatos.Repositorio;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Mensajeria")
    ?? throw new InvalidOperationException("No se encontró ConnectionStrings:Mensajeria en appsettings.json.");

builder.Services.AddDbContext<MensajeriaDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IMensajeroService, MensajeroService>();
builder.Services.AddScoped<IDireccionService, DireccionService>();
builder.Services.AddScoped<IEnvioService, EnvioService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
