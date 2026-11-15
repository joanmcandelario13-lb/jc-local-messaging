using System.ComponentModel.DataAnnotations;
namespace SistemadeMensajeria.Services.Dtos;
public record ClienteDto(int ClienteId, string Nombres, string Apellidos, string Telefono, string Correo, DateTime FechaRegistro);
public class ClienteRequestDto
{
    [Required, StringLength(100, MinimumLength = 2)] public string Nombres { get; set; } = string.Empty;
    [Required, StringLength(100, MinimumLength = 2)] public string Apellidos { get; set; } = string.Empty;
    [Required, Phone, StringLength(25)] public string Telefono { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(150)] public string Correo { get; set; } = string.Empty;
}
