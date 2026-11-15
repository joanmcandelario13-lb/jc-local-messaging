using Microsoft.AspNetCore.Mvc;
using SistemadeMensajeria.Domain.Entidades;
using SistemadeMensajeria.Domain.Interfaces.Repositorios;
using SistemadeMensajeria.Infrastructure.AccesoDatos.Modelos;

namespace SistemadeMensajeria.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MensajerosController(IRepositorio<Mensajero> repositorio) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MensajeroDto>>> ObtenerTodos(CancellationToken ct)
    {
        var items = await repositorio.ObtenerTodosAsync(ct);
        return Ok(items.Select(Mapear));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MensajeroDto>> ObtenerPorId(int id, CancellationToken ct)
    {
        var item = await repositorio.ObtenerPorIdAsync(id, ct);
        return item is null ? NotFound() : Ok(Mapear(item));
    }

    [HttpPost]
    public async Task<ActionResult<MensajeroDto>> Crear(CrearMensajeroDto dto, CancellationToken ct)
    {
        var entidad = new Mensajero
        {
            Nombres = dto.Nombres, Apellidos = dto.Apellidos, Telefono = dto.Telefono, Identificacion = dto.Identificacion, Disponible = dto.Disponible
        };
        var creado = await repositorio.AgregarAsync(entidad, ct);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.MensajeroId }, Mapear(creado));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, ActualizarMensajeroDto dto, CancellationToken ct)
    {
        var entidad = await repositorio.ObtenerPorIdAsync(id, ct);
        if (entidad is null) return NotFound();
        entidad.Nombres = dto.Nombres; entidad.Apellidos = dto.Apellidos; entidad.Telefono = dto.Telefono; entidad.Identificacion = dto.Identificacion; entidad.Disponible = dto.Disponible;
        await repositorio.ActualizarAsync(entidad, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id, CancellationToken ct)
        => await repositorio.EliminarAsync(id, ct) ? NoContent() : NotFound();

    private static MensajeroDto Mapear(Mensajero x) => new(x.MensajeroId, x.Nombres, x.Apellidos, x.Telefono, x.Identificacion, x.Disponible);
}
