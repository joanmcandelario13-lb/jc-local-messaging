using SistemadeMensajeria.Application.Dtos;
using SistemadeMensajeria.Application.Core;
namespace SistemadeMensajeria.Application.Contract;
public interface IClienteService { Task<IReadOnlyList<ClienteDto>> ObtenerTodosAsync(CancellationToken ct=default); Task<ClienteDto?> ObtenerPorIdAsync(int id,CancellationToken ct=default); Task<ClienteDto> CrearAsync(ClienteRequestDto dto,CancellationToken ct=default); Task<bool> ActualizarAsync(int id,ClienteRequestDto dto,CancellationToken ct=default); Task<bool> EliminarAsync(int id,CancellationToken ct=default); }
