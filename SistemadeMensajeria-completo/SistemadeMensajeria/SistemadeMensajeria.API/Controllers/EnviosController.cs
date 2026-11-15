using Microsoft.AspNetCore.Mvc;
using SistemadeMensajeria.Domain.Entidades;
using SistemadeMensajeria.Domain.Interfaces.Repositorios;
using SistemadeMensajeria.Infrastructure.AccesoDatos.Modelos;

namespace SistemadeMensajeria.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnviosController(IRepositorio<Envio> repositorio) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EnvioDto>>> ObtenerTodos(CancellationToken ct)
    {
        var items = await repositorio.ObtenerTodosAsync(ct);
        return Ok(items.Select(Mapear));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EnvioDto>> ObtenerPorId(int id, CancellationToken ct)
    {
        var item = await repositorio.ObtenerPorIdAsync(id, ct);
        return item is null ? NotFound() : Ok(Mapear(item));
    }

    [HttpPost]
    public async Task<ActionResult<EnvioDto>> Crear(CrearEnvioDto dto, CancellationToken ct)
    {
        var entidad = new Envio
        {
            ClienteId = dto.ClienteId, MensajeroId = dto.MensajeroId, DireccionOrigenId = dto.DireccionOrigenId, DireccionDestinoId = dto.DireccionDestinoId, Descripcion = dto.Descripcion, Costo = dto.Costo, Estado = dto.Estado
        };
        var creado = await repositorio.AgregarAsync(entidad, ct);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.EnvioId }, Mapear(creado));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, ActualizarEnvioDto dto, CancellationToken ct)
    {
        var entidad = await repositorio.ObtenerPorIdAsync(id, ct);
        if (entidad is null) return NotFound();
        entidad.ClienteId = dto.ClienteId; entidad.MensajeroId = dto.MensajeroId; entidad.DireccionOrigenId = dto.DireccionOrigenId; entidad.DireccionDestinoId = dto.DireccionDestinoId; entidad.Descripcion = dto.Descripcion; entidad.Costo = dto.Costo; entidad.Estado = dto.Estado; entidad.FechaEntrega = dto.FechaEntrega;
        await repositorio.ActualizarAsync(entidad, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id, CancellationToken ct)
        => await repositorio.EliminarAsync(id, ct) ? NoContent() : NotFound();

    private static EnvioDto Mapear(Envio x) => new(x.EnvioId, x.ClienteId, x.MensajeroId, x.DireccionOrigenId, x.DireccionDestinoId, x.Descripcion, x.Costo, x.Estado, x.FechaCreacion, x.FechaEntrega);
}
