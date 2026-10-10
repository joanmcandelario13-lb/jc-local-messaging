using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMensajeriaAPI.Data;
using SistemaMensajeriaAPI.DTOs;
using WebApplication1.Models;

namespace SistemaMensajeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensajerosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MensajerosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Mensajeros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MensajeroDTO>>> GetMensajeros()
        {
            var mensajeros = await _context.Mensajeros
                .Select(m => new MensajeroDTO
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    Apellido = m.Apellido,
                    Telefono = m.Telefono,
                    Estado = m.Estado
                })
                .ToListAsync();

            return Ok(mensajeros);
        }

        // GET: api/Mensajeros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MensajeroDTO>> GetMensajero(int id)
        {
            var mensajero = await _context.Mensajeros
                .Where(m => m.Id == id)
                .Select(m => new MensajeroDTO
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    Apellido = m.Apellido,
                    Telefono = m.Telefono,
                    Estado = m.Estado
                })
                .FirstOrDefaultAsync();

            if (mensajero == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el mensajero."
                });
            }

            return Ok(mensajero);
        }

        // POST: api/Mensajeros
        [HttpPost]
        public async Task<ActionResult<MensajeroDTO>> CrearMensajero(
            CrearMensajeroDTO dto)
        {
            var mensajero = new Mensajero
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono,
                Estado = dto.Estado
            };

            _context.Mensajeros.Add(mensajero);
            await _context.SaveChangesAsync();

            var respuesta = new MensajeroDTO
            {
                Id = mensajero.Id,
                Nombre = mensajero.Nombre,
                Apellido = mensajero.Apellido,
                Telefono = mensajero.Telefono,
                Estado = mensajero.Estado
            };

            return CreatedAtAction(
                nameof(GetMensajero),
                new { id = mensajero.Id },
                respuesta
            );
        }

        // PUT: api/Mensajeros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarMensajero(
            int id,
            CrearMensajeroDTO dto)
        {
            var mensajero = await _context.Mensajeros.FindAsync(id);

            if (mensajero == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el mensajero."
                });
            }

            mensajero.Nombre = dto.Nombre;
            mensajero.Apellido = dto.Apellido;
            mensajero.Telefono = dto.Telefono;
            mensajero.Estado = dto.Estado;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Mensajeros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMensajero(int id)
        {
            var mensajero = await _context.Mensajeros
                .Include(m => m.Envios)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mensajero == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el mensajero."
                });
            }

            if (mensajero.Envios.Any())
            {
                return BadRequest(new
                {
                    mensaje = "No se puede eliminar el mensajero porque tiene envíos asociados."
                });
            }

            _context.Mensajeros.Remove(mensajero);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}