namespace SistemaMensajeriaAPI.DTOs
{
    public class DireccionDTO
    {
        public int Id { get; set; }

        public string Calle { get; set; } = string.Empty;

        public string Sector { get; set; } = string.Empty;

        public string Ciudad { get; set; } = string.Empty;

        public string? Referencia { get; set; }
    }
}