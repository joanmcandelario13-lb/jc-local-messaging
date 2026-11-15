namespace SistemadeMensajeria.Domain.Entidades;

public class Cliente
{
    public int ClienteId { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    public ICollection<Direccion> Direcciones { get; set; } = new List<Direccion>();
    public ICollection<Envio> Envios { get; set; } = new List<Envio>();
}
