using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Direccion
    {
        [Key]
        public int Id { get; set; }

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
