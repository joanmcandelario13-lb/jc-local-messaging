namespace SistemadeMensajeria.Domain.Entidades;

public class Direccion
{
    public int DireccionId { get; set; }
    public int ClienteId { get; set; }
    public string Calle { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Sector { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public string Referencia { get; set; } = string.Empty;
    public Cliente? Cliente { get; set; }
}
