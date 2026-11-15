using Microsoft.AspNetCore.Mvc;
using SistemadeMensajeria.Services.Contract;
using SistemadeMensajeria.Services.Dtos;
namespace SistemadeMensajeria.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ClientesController(IClienteService service) : ControllerBase
{
 [HttpGet] public async Task<ActionResult<IEnumerable<ClienteDto>>> ObtenerTodos(CancellationToken ct)=>Ok(await service.ObtenerTodosAsync(ct));
 [HttpGet("{id:int}")] public async Task<ActionResult<ClienteDto>> ObtenerPorId(int id,CancellationToken ct){var x=await service.ObtenerPorIdAsync(id,ct);return x is null?NotFound():Ok(x);}
 [HttpPost] public async Task<ActionResult<ClienteDto>> Crear(ClienteRequestDto dto,CancellationToken ct){var x=await service.CrearAsync(dto,ct);return CreatedAtAction(nameof(ObtenerPorId),new{id=x.ClienteId},x);}
 [HttpPut("{id:int}")] public async Task<IActionResult> Actualizar(int id,ClienteRequestDto dto,CancellationToken ct)=>await service.ActualizarAsync(id,dto,ct)?NoContent():NotFound();
 [HttpDelete("{id:int}")] public async Task<IActionResult> Eliminar(int id,CancellationToken ct)=>await service.EliminarAsync(id,ct)?NoContent():NotFound();
}
