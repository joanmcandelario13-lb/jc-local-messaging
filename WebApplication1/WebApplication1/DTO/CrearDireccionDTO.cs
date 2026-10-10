using System.ComponentModel.DataAnnotations;

namespace SistemaMensajeriaAPI.DTOs
{
    public class CrearDireccionDTO
    {
        [Required]
        [StringLength(150)]
        public string Calle { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Sector { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Ciudad { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Referencia { get; set; }
    }
}