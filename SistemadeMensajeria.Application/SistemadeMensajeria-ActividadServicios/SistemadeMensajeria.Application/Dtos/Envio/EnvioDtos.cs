using System.ComponentModel.DataAnnotations;
namespace SistemadeMensajeria.Application.Dtos;
public record EnvioDto(int EnvioId, int ClienteId, int MensajeroId, int DireccionOrigenId, int DireccionDestinoId,
    string Descripcion, decimal Costo, string Estado, DateTime FechaCreacion, DateTime? FechaEntrega);
public class EnvioRequestDto
{
    [Range(1, int.MaxValue)] public int ClienteId { get; set; }
    [Range(1, int.MaxValue)] public int MensajeroId { get; set; }
    [Range(1, int.MaxValue)] public int DireccionOrigenId { get; set; }
    [Range(1, int.MaxValue)] public int DireccionDestinoId { get; set; }
    [Required, StringLength(500, MinimumLength = 3)] public string Descripcion { get; set; } = string.Empty;
    [Range(typeof(decimal), "0.01", "9999999999")] public decimal Costo { get; set; }
    [Required, RegularExpression("^(Pendiente|En proceso|Entregado|Cancelado)$",
        ErrorMessage = "Estado debe ser Pendiente, En proceso, Entregado o Cancelado.")]
    public string Estado { get; set; } = "Pendiente";
    public DateTime? FechaEntrega { get; set; }
}
