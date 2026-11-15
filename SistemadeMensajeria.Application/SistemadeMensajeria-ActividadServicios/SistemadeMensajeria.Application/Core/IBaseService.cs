namespace SistemadeMensajeria.Application.Core;

/// <summary>Contrato genérico de operaciones CRUD para servicios de negocio.</summary>
public interface IBaseService<TDto, TCreateDto, TUpdateDto>
{
    Task<IReadOnlyList<TDto>> ObtenerTodosAsync(CancellationToken ct = default);
    Task<TDto?> ObtenerPorIdAsync(int id, CancellationToken ct = default);
    Task<TDto> CrearAsync(TCreateDto dto, CancellationToken ct = default);
    Task<bool> ActualizarAsync(int id, TUpdateDto dto, CancellationToken ct = default);
    Task<bool> EliminarAsync(int id, CancellationToken ct = default);
}
