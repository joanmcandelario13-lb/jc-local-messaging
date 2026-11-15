using Microsoft.AspNetCore.Mvc;
using SistemadeMensajeria.Services.Contract;
using SistemadeMensajeria.Services.Dtos;
namespace SistemadeMensajeria.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DireccionesController(IDireccionService service) : ControllerBase
{
 [HttpGet] public async Task<ActionResult<IEnumerable<DireccionDto>>> ObtenerTodos(CancellationToken ct)=>Ok(await service.ObtenerTodosAsync(ct));
 [HttpGet("{id:int}")] public async Task<ActionResult<DireccionDto>> ObtenerPorId(int id,CancellationToken ct){var x=await service.ObtenerPorIdAsync(id,ct);return x is null?NotFound():Ok(x);}
 [HttpPost] public async Task<ActionResult<DireccionDto>> Crear(DireccionRequestDto dto,CancellationToken ct){try{var x=await service.CrearAsync(dto,ct);return CreatedAtAction(nameof(ObtenerPorId),new{id=x.DireccionId},x);}catch(ArgumentException ex){return BadRequest(new{mensaje=ex.Message});}}
 [HttpPut("{id:int}")] public async Task<IActionResult> Actualizar(int id,DireccionRequestDto dto,CancellationToken ct){try{return await service.ActualizarAsync(id,dto,ct)?NoContent():NotFound();}catch(ArgumentException ex){return BadRequest(new{mensaje=ex.Message});}}
 [HttpDelete("{id:int}")] public async Task<IActionResult> Eliminar(int id,CancellationToken ct)=>await service.EliminarAsync(id,ct)?NoContent():NotFound();
}
