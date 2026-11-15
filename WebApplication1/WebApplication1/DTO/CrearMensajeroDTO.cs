using System.ComponentModel.DataAnnotations;

namespace SistemaMensajeriaAPI.DTOs
{
    public class CrearMensajeroDTO
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(Disponible|Inactivo)$")]
        public string Estado { get; set; } = "Disponible";
    }
}