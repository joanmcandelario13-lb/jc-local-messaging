using System.ComponentModel.DataAnnotations;
namespace SistemadeMensajeria.Application.Dtos;
public record MensajeroDto(int MensajeroId, string Nombres, string Apellidos, string Telefono, string Identificacion, bool Disponible);
public class MensajeroRequestDto
{
    [Required, StringLength(100, MinimumLength = 2)] public string Nombres { get; set; } = string.Empty;
    [Required, StringLength(100, MinimumLength = 2)] public string Apellidos { get; set; } = string.Empty;
    [Required, Phone, StringLength(25)] public string Telefono { get; set; } = string.Empty;
    [Required, StringLength(40, MinimumLength = 5)] public string Identificacion { get; set; } = string.Empty;
    public bool Disponible { get; set; } = true;
}
