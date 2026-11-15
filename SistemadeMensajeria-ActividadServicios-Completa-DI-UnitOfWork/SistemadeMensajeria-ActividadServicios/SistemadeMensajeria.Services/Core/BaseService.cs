namespace SistemadeMensajeria.Services.Core;

/// <summary>Clase base común para servicios de negocio.</summary>
public abstract class BaseService
{
    protected static string Normalizar(string? valor) => valor?.Trim() ?? string.Empty;

    protected static void ValidarIdentificador(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "El identificador debe ser mayor que cero.");
    }
}
