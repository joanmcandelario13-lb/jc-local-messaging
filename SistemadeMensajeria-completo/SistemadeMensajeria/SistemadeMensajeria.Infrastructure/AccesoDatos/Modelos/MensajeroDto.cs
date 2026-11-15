namespace SistemadeMensajeria.Infrastructure.AccesoDatos.Modelos;

public record MensajeroDto(int MensajeroId, string Nombres, string Apellidos, string Telefono, string Identificacion, bool Disponible);
public record CrearMensajeroDto(string Nombres, string Apellidos, string Telefono, string Identificacion, bool Disponible = true);
public record ActualizarMensajeroDto(string Nombres, string Apellidos, string Telefono, string Identificacion, bool Disponible);
