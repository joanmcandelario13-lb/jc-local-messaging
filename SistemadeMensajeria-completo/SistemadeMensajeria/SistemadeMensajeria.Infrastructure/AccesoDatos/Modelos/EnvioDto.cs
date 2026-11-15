namespace SistemadeMensajeria.Infrastructure.AccesoDatos.Modelos;

public record EnvioDto(int EnvioId, int ClienteId, int MensajeroId, int DireccionOrigenId, int DireccionDestinoId,
    string Descripcion, decimal Costo, string Estado, DateTime FechaCreacion, DateTime? FechaEntrega);
public record CrearEnvioDto(int ClienteId, int MensajeroId, int DireccionOrigenId, int DireccionDestinoId,
    string Descripcion, decimal Costo, string Estado = "Pendiente");
public record ActualizarEnvioDto(int ClienteId, int MensajeroId, int DireccionOrigenId, int DireccionDestinoId,
    string Descripcion, decimal Costo, string Estado, DateTime? FechaEntrega);
