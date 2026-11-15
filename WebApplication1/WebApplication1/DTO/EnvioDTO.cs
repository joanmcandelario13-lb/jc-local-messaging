namespace SistemaMensajeriaAPI.DTOs
{
    public class EnvioDTO
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public string NombreCliente { get; set; } = string.Empty;

        public int MensajeroId { get; set; }

        public string NombreMensajero { get; set; } = string.Empty;

        public int DireccionOrigenId { get; set; }

        public string DireccionOrigen { get; set; } = string.Empty;

        public int DireccionDestinoId { get; set; }

        public string DireccionDestino { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; }

        public DateTime? FechaEntrega { get; set; }

        public decimal Tarifa { get; set; }

        public string Estado { get; set; } = string.Empty;
    }
}