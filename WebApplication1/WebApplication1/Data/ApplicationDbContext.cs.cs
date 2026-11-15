using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace SistemaMensajeriaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Mensajero> Mensajeros { get; set; }

        public DbSet<Direccion> Direcciones { get; set; }

        public DbSet<Envio> Envios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Envio>()
                .HasOne(e => e.Cliente)
                .WithMany(c => c.Envios)
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Envio>()
                .HasOne(e => e.Mensajero)
                .WithMany(m => m.Envios)
                .HasForeignKey(e => e.MensajeroId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Envio>()
                .HasOne(e => e.DireccionOrigen)
                .WithMany()
                .HasForeignKey(e => e.DireccionOrigenId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Envio>()
                .HasOne(e => e.DireccionDestino)
                .WithMany()
                .HasForeignKey(e => e.DireccionDestinoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}