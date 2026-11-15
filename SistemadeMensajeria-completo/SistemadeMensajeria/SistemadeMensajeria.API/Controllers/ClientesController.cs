using Microsoft.AspNetCore.Mvc;
using SistemadeMensajeria.Domain.Entidades;
using SistemadeMensajeria.Domain.Interfaces.Repositorios;
using SistemadeMensajeria.Infrastructure.AccesoDatos.Modelos;

namespace SistemadeMensajeria.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController(IRepositorio<Cliente> repositorio) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> ObtenerTodos(CancellationToken ct)
    {
        var items = await repositorio.ObtenerTodosAsync(ct);
        return Ok(items.Select(Mapear));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClienteDto>> ObtenerPorId(int id, CancellationToken ct)
    {
        var item = await repositorio.ObtenerPorIdAsync(id, ct);
        return item is null ? NotFound() : Ok(Mapear(item));
    }

    [HttpPost]
    public async Task<ActionResult<ClienteDto>> Crear(CrearClienteDto dto, CancellationToken ct)
    {
        var entidad = new Cliente
        {
            Nombres = dto.Nombres, Apellidos = dto.Apellidos, Telefono = dto.Telefono, Correo = dto.Correo
        };
        var creado = await repositorio.AgregarAsync(entidad, ct);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.ClienteId }, Mapear(creado));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, ActualizarClienteDto dto, CancellationToken ct)
    {
        var entidad = await repositorio.ObtenerPorIdAsync(id, ct);
        if (entidad is null) return NotFound();
        entidad.Nombres = dto.Nombres; entidad.Apellidos = dto.Apellidos; entidad.Telefono = dto.Telefono; entidad.Correo = dto.Correo;
        await repositorio.ActualizarAsync(entidad, ct);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id, CancellationToken ct)
        => await repositorio.EliminarAsync(id, ct) ? NoContent() : NotFound();

    private static ClienteDto Mapear(Cliente x) => new(x.ClienteId, x.Nombres, x.Apellidos, x.Telefono, x.Correo, x.FechaRegistro);
}
