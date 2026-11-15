namespace SistemadeMensajeria.Domain.Entidades;

public class Envio
{
    public int EnvioId { get; set; }
    public int ClienteId { get; set; }
    public int MensajeroId { get; set; }
    public int DireccionOrigenId { get; set; }
    public int DireccionDestinoId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal Costo { get; set; }
    public string Estado { get; set; } = "Pendiente";
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaEntrega { get; set; }
    public Cliente? Cliente { get; set; }
    public Mensajero? Mensajero { get; set; }
    public Direccion? DireccionOrigen { get; set; }
    public Direccion? DireccionDestino { get; set; }
}
