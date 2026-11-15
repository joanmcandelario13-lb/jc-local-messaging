using SistemadeMensajeria.Application.Dtos;
using SistemadeMensajeria.Application.Core;
namespace SistemadeMensajeria.Application.Contract;
public interface IDireccionService { Task<IReadOnlyList<DireccionDto>> ObtenerTodosAsync(CancellationToken ct=default); Task<DireccionDto?> ObtenerPorIdAsync(int id,CancellationToken ct=default); Task<DireccionDto> CrearAsync(DireccionRequestDto dto,CancellationToken ct=default); Task<bool> ActualizarAsync(int id,DireccionRequestDto dto,CancellationToken ct=default); Task<bool> EliminarAsync(int id,CancellationToken ct=default); }
