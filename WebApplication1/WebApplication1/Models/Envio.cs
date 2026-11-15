using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Envio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int MensajeroId { get; set; }

        [Required]
        public int DireccionOrigenId { get; set; }

        [Required]
        public int DireccionDestinoId { get; set; }

        public DateTime FechaRegistro { get; set; }
            = DateTime.Now;

        public DateTime? FechaEntrega { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(typeof(decimal), "0.01", "99999999.99")]
        public decimal Tarifa { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = "Pendiente";

        public Cliente? Cliente { get; set; }

        public Mensajero? Mensajero { get; set; }

        [ForeignKey(nameof(DireccionOrigenId))]
        public Direccion? DireccionOrigen { get; set; }

        [ForeignKey(nameof(DireccionDestinoId))]
        public Direccion? DireccionDestino { get; set; }
    }
}
