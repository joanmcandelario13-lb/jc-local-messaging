using System.ComponentModel.DataAnnotations;
namespace SistemadeMensajeria.Services.Dtos;
public record DireccionDto(int DireccionId, int ClienteId, string Calle, string Numero, string Sector, string Ciudad, string Referencia);
public class DireccionRequestDto
{
    [Range(1, int.MaxValue)] public int ClienteId { get; set; }
    [Required, StringLength(150, MinimumLength = 3)] public string Calle { get; set; } = string.Empty;
    [Required, StringLength(20)] public string Numero { get; set; } = string.Empty;
    [Required, StringLength(100)] public string Sector { get; set; } = string.Empty;
    [Required, StringLength(100)] public string Ciudad { get; set; } = string.Empty;
    [StringLength(300)] public string Referencia { get; set; } = string.Empty;
}
