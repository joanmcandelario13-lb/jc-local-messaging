namespace SistemadeMensajeria.Domain.Interfaces.Repositorios;

public interface IRepositorio<T> where T : class
{
    Task<IReadOnlyList<T>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
    Task<T?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default);
    Task<T> AgregarAsync(T entidad, CancellationToken cancellationToken = default);
    Task ActualizarAsync(T entidad, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default);
}
