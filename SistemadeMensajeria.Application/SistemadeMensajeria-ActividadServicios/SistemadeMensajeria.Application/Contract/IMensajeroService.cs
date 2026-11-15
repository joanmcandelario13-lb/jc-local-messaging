using SistemadeMensajeria.Application.Dtos;
using SistemadeMensajeria.Application.Core;
namespace SistemadeMensajeria.Application.Contract;
public interface IMensajeroService { Task<IReadOnlyList<MensajeroDto>> ObtenerTodosAsync(CancellationToken ct=default); Task<MensajeroDto?> ObtenerPorIdAsync(int id,CancellationToken ct=default); Task<MensajeroDto> CrearAsync(MensajeroRequestDto dto,CancellationToken ct=default); Task<bool> ActualizarAsync(int id,MensajeroRequestDto dto,CancellationToken ct=default); Task<bool> EliminarAsync(int id,CancellationToken ct=default); }
