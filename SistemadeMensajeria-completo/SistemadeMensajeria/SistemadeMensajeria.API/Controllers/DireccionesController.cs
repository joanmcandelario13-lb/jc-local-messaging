using Microsoft.AspNetCore.Mvc;
using SistemadeMensajeria.Domain.Entidades;
using SistemadeMensajeria.Domain.Interfaces.Repositorios;
using SistemadeMensajeria.Infrastructure.AccesoDatos.Modelos;

namespace SistemadeMensajeria.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DireccionesController(IRepositorio<Direccion> repositorio) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DireccionDto>>> ObtenerTodos(CancellationToken ct)
    {
        var items = await repositorio.ObtenerTodosAsync(ct);
        return Ok(items.Select(Mapear));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DireccionDto>> ObtenerPorId(int id, CancellationToken ct)
    {
        var item = await repositorio.ObtenerPorIdAsync(id, ct);
        return item is null ? NotFound() : Ok(Mapear(item));
    }

    [HttpPost]
    public async Task<ActionResult<DireccionDto>> Crear(CrearDireccionDto dto, CancellationToken ct)
    {
        var entidad = new Direccion
        {
            ClienteId = dto.ClienteId, Calle = dto.Calle, Numero = dto.Numero, Sector = dto.Sector, Ciudad = dto.Ciudad, Referencia = dto.Referencia
        };
        var creado = await repositorio.AgregarAsync(entidad, ct);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.DireccionId }, Mapear(creado));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, ActualizarDireccionDto dto, CancellationToken ct)
    {
        var entidad = await repositorio.ObtenerPorIdAsync(id, ct);
        if (entidad is null) return NotFound();
        entidad.ClienteId = dto.ClienteId; entidad.Calle = dto.Calle; entidad.Numero = dto.Numero; entidad.Sector = dto.Sector; entidad.Ciudad = dto.Ciudad; entidad.Referencia = dto.Referencia;
        await repositorio.ActualizarAsync(entidad, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id, CancellationToken ct)
        => await repositorio.EliminarAsync(id, ct) ? NoContent() : NotFound();

    private static DireccionDto Mapear(Direccion x) => new(x.DireccionId, x.ClienteId, x.Calle, x.Numero, x.Sector, x.Ciudad, x.Referencia);
}
