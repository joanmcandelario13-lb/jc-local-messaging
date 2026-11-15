namespace SistemadeMensajeria.Domain.Interfaces.Repositorios;

/// <summary>
/// Coordina la persistencia de los cambios realizados por los repositorios
/// dentro de una misma unidad de trabajo.
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
