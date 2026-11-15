namespace SistemadeMensajeria.Domain.Entidades;

public class Mensajero
{
    public int MensajeroId { get; set; }
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Identificacion { get; set; } = string.Empty;
    public bool Disponible { get; set; } = true;
    public ICollection<Envio> Envios { get; set; } = new List<Envio>();
}
