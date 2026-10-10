using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMensajeriaAPI.Data;
using SistemaMensajeriaAPI.DTOs;
using WebApplication1.Models;

namespace SistemaMensajeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DireccionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Direcciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DireccionDTO>>> GetDirecciones()
        {
            var direcciones = await _context.Direcciones
                .Select(d => new DireccionDTO
                {
                    Id = d.Id,
                    Calle = d.Calle,
                    Sector = d.Sector,
                    Ciudad = d.Ciudad,
                    Referencia = d.Referencia
                })
                .ToListAsync();

            return Ok(direcciones);
        }

        // GET: api/Direcciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DireccionDTO>> GetDireccion(int id)
        {
            var direccion = await _context.Direcciones
                .Where(d => d.Id == id)
                .Select(d => new DireccionDTO
                {
                    Id = d.Id,
                    Calle = d.Calle,
                    Sector = d.Sector,
                    Ciudad = d.Ciudad,
                    Referencia = d.Referencia
                })
                .FirstOrDefaultAsync();

            if (direccion == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la dirección."
                });
            }

            return Ok(direccion);
        }

        // POST: api/Direcciones
        [HttpPost]
        public async Task<ActionResult<DireccionDTO>> CrearDireccion(
            CrearDireccionDTO dto)
        {
            var direccion = new Direccion
            {
                Calle = dto.Calle,
                Sector = dto.Sector,
                Ciudad = dto.Ciudad,
                Referencia = dto.Referencia
            };

            _context.Direcciones.Add(direccion);
            await _context.SaveChangesAsync();

            var respuesta = new DireccionDTO
            {
                Id = direccion.Id,
                Calle = direccion.Calle,
                Sector = direccion.Sector,
                Ciudad = direccion.Ciudad,
                Referencia = direccion.Referencia
            };

            return CreatedAtAction(
                nameof(GetDireccion),
                new { id = direccion.Id },
                respuesta
            );
        }

        // PUT: api/Direcciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDireccion(
            int id,
            CrearDireccionDTO dto)
        {
            var direccion = await _context.Direcciones.FindAsync(id);

            if (direccion == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la dirección."
                });
            }

            direccion.Calle = dto.Calle;
            direccion.Sector = dto.Sector;
            direccion.Ciudad = dto.Ciudad;
            direccion.Referencia = dto.Referencia;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Direcciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDireccion(int id)
        {
            var direccion = await _context.Direcciones
                .FirstOrDefaultAsync(d => d.Id == id);

            if (direccion == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró la dirección."
                });
            }

            bool tieneEnvios = await _context.Envios.AnyAsync(e =>
                e.DireccionOrigenId == id ||
                e.DireccionDestinoId == id);

            if (tieneEnvios)
            {
                return BadRequest(new
                {
                    mensaje = "No se puede eliminar la dirección porque está asociada a uno o más envíos."
                });
            }

            _context.Direcciones.Remove(direccion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

