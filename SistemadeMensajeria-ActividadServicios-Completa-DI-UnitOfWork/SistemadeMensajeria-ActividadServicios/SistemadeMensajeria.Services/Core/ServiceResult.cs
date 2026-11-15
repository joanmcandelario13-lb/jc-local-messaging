namespace SistemadeMensajeria.Services.Core;

/// <summary>Representa el resultado de una operación de servicio.</summary>
public class ServiceResult
{
    public bool IsSuccess { get; init; }
    public string Message { get; init; } = string.Empty;
    public IReadOnlyList<string> Errors { get; init; } = Array.Empty<string>();

    public static ServiceResult Success(string message = "Operación realizada correctamente.")
        => new() { IsSuccess = true, Message = message };

    public static ServiceResult Failure(params string[] errors)
        => new() { IsSuccess = false, Message = "La operación no pudo completarse.", Errors = errors };
}
