using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Mensajero
    {
        [Key]
        public int Id { get; set; }

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
        [StringLength(20)]
        public string Estado { get; set; } = "Disponible";

        public ICollection<Envio> Envios { get; set; }
            = new List<Envio>();
    }
}
