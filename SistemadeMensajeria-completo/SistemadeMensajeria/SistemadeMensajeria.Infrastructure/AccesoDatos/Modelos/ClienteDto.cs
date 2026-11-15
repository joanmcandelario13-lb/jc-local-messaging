namespace SistemadeMensajeria.Infrastructure.AccesoDatos.Modelos;

public record ClienteDto(int ClienteId, string Nombres, string Apellidos, string Telefono, string Correo, DateTime FechaRegistro);
public record CrearClienteDto(string Nombres, string Apellidos, string Telefono, string Correo);
public record ActualizarClienteDto(string Nombres, string Apellidos, string Telefono, string Correo);
