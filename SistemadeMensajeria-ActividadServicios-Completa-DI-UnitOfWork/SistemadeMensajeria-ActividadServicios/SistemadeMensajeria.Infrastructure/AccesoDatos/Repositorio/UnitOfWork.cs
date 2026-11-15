using SistemadeMensajeria.Domain.Interfaces.Repositorios;
using SistemadeMensajeria.Infrastructure.AccesoDatos.Contexto;

namespace SistemadeMensajeria.Infrastructure.AccesoDatos.Repositorio;

/// <summary>Implementación de Unit of Work usando el DbContext compartido por el request.</summary>
public sealed class UnitOfWork(MensajeriaDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => context.SaveChangesAsync(cancellationToken);
}
