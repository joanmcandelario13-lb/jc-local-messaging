namespace SistemadeMensajeria.Infrastructure.AccesoDatos.Modelos;

public record DireccionDto(int DireccionId, int ClienteId, string Calle, string Numero, string Sector, string Ciudad, string Referencia);
public record CrearDireccionDto(int ClienteId, string Calle, string Numero, string Sector, string Ciudad, string Referencia);
public record ActualizarDireccionDto(int ClienteId, string Calle, string Numero, string Sector, string Ciudad, string Referencia);
