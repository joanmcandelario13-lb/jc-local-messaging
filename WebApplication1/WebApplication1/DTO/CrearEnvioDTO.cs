using System.ComponentModel.DataAnnotations;

namespace SistemaMensajeriaAPI.DTOs
{
    public class CrearEnvioDTO
    {
        [Range(1, int.MaxValue)]
        public int ClienteId { get; set; }

        [Range(1, int.MaxValue)]
        public int MensajeroId { get; set; }

        [Range(1, int.MaxValue)]
        public int DireccionOrigenId { get; set; }

        [Range(1, int.MaxValue)]
        public int DireccionDestinoId { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "99999999.99")]
        public decimal Tarifa { get; set; }

        [Required]
        [RegularExpression("^(Pendiente|En tránsito|Entregado|Cancelado)$")]
        public string Estado { get; set; } = "Pendiente";

        public DateTime? FechaEntrega { get; set; }
    }
}