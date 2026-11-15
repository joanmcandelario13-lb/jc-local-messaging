using Microsoft.EntityFrameworkCore;
using SistemadeMensajeria.Domain.Interfaces.Repositorios;
using SistemadeMensajeria.Infrastructure.AccesoDatos.Contexto;

namespace SistemadeMensajeria.Infrastructure.AccesoDatos.Repositorio;

public class Repositorio<T>(MensajeriaDbContext context) : IRepositorio<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<IReadOnlyList<T>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<T?> ObtenerPorIdAsync(int id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync([id], cancellationToken);

    public async Task<T> AgregarAsync(T entidad, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entidad, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entidad;
    }

    public async Task ActualizarAsync(T entidad, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entidad);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> EliminarAsync(int id, CancellationToken cancellationToken = default)
    {
        var entidad = await _dbSet.FindAsync([id], cancellationToken);
        if (entidad is null) return false;
        _dbSet.Remove(entidad);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
