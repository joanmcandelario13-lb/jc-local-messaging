using Microsoft.EntityFrameworkCore;
using SistemadeMensajeria.Domain.Entidades;

namespace SistemadeMensajeria.Infrastructure.AccesoDatos.Contexto;

public class MensajeriaDbContext(DbContextOptions<MensajeriaDbContext> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Mensajero> Mensajeros => Set<Mensajero>();
    public DbSet<Direccion> Direcciones => Set<Direccion>();
    public DbSet<Envio> Envios => Set<Envio>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(e =>
        {
            e.HasKey(x => x.ClienteId);
            e.Property(x => x.Nombres).HasMaxLength(100).IsRequired();
            e.Property(x => x.Apellidos).HasMaxLength(100).IsRequired();
            e.Property(x => x.Telefono).HasMaxLength(25).IsRequired();
            e.Property(x => x.Correo).HasMaxLength(150).IsRequired();
            e.HasIndex(x => x.Correo).IsUnique();
        });

        modelBuilder.Entity<Mensajero>(e =>
        {
            e.HasKey(x => x.MensajeroId);
            e.Property(x => x.Nombres).HasMaxLength(100).IsRequired();
            e.Property(x => x.Apellidos).HasMaxLength(100).IsRequired();
            e.Property(x => x.Telefono).HasMaxLength(25).IsRequired();
            e.Property(x => x.Identificacion).HasMaxLength(40).IsRequired();
            e.HasIndex(x => x.Identificacion).IsUnique();
        });

        modelBuilder.Entity<Direccion>(e =>
        {
            e.HasKey(x => x.DireccionId);
            e.Property(x => x.Calle).HasMaxLength(150).IsRequired();
            e.Property(x => x.Numero).HasMaxLength(20);
            e.Property(x => x.Sector).HasMaxLength(100);
            e.Property(x => x.Ciudad).HasMaxLength(100).IsRequired();
            e.Property(x => x.Referencia).HasMaxLength(300);
            e.HasOne(x => x.Cliente).WithMany(x => x.Direcciones)
                .HasForeignKey(x => x.ClienteId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Envio>(e =>
        {
            e.HasKey(x => x.EnvioId);
            e.Property(x => x.Descripcion).HasMaxLength(500).IsRequired();
            e.Property(x => x.Costo).HasColumnType("decimal(12,2)");
            e.Property(x => x.Estado).HasMaxLength(30).IsRequired();
            e.HasOne(x => x.Cliente).WithMany(x => x.Envios).HasForeignKey(x => x.ClienteId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Mensajero).WithMany(x => x.Envios).HasForeignKey(x => x.MensajeroId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.DireccionOrigen).WithMany().HasForeignKey(x => x.DireccionOrigenId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.DireccionDestino).WithMany().HasForeignKey(x => x.DireccionDestinoId).OnDelete(DeleteBehavior.Restrict);
        });
    }
}
