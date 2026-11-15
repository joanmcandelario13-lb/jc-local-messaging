using SistemadeMensajeria.Services.Dtos;
using SistemadeMensajeria.Services.Core;
namespace SistemadeMensajeria.Services.Contract;
public interface IEnvioService { Task<IReadOnlyList<EnvioDto>> ObtenerTodosAsync(CancellationToken ct=default); Task<EnvioDto?> ObtenerPorIdAsync(int id,CancellationToken ct=default); Task<EnvioDto> CrearAsync(EnvioRequestDto dto,CancellationToken ct=default); Task<bool> ActualizarAsync(int id,EnvioRequestDto dto,CancellationToken ct=default); Task<bool> EliminarAsync(int id,CancellationToken ct=default); }
