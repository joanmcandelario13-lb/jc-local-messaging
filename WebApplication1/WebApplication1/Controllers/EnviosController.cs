using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMensajeriaAPI.Data;
using SistemaMensajeriaAPI.DTOs;
using WebApplication1.Models;

namespace SistemaMensajeriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnviosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnviosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Envios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnvioDTO>>> GetEnvios()
        {
            var envios = await _context.Envios
                .Select(e => new EnvioDTO
                {
                    Id = e.Id,
                    ClienteId = e.ClienteId,
                    MensajeroId = e.MensajeroId,
                    DireccionOrigenId = e.DireccionOrigenId,
                    DireccionDestinoId = e.DireccionDestinoId,
                    FechaRegistro = e.FechaRegistro,
                    FechaEntrega = e.FechaEntrega,
                    Tarifa = e.Tarifa,
                    Estado = e.Estado
                })
                .ToListAsync();

            return Ok(envios);
        }

        // GET: api/Envios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnvioDTO>> GetEnvio(int id)
        {
            var envio = await _context.Envios
                .Where(e => e.Id == id)
                .Select(e => new EnvioDTO
                {
                    Id = e.Id,
                    ClienteId = e.ClienteId,
                    MensajeroId = e.MensajeroId,
                    DireccionOrigenId = e.DireccionOrigenId,
                    DireccionDestinoId = e.DireccionDestinoId,
                    FechaRegistro = e.FechaRegistro,
                    FechaEntrega = e.FechaEntrega,
                    Tarifa = e.Tarifa,
                    Estado = e.Estado
                })
                .FirstOrDefaultAsync();

            if (envio == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el envío."
                });
            }

            return Ok(envio);
        }

        // POST: api/Envios
        [HttpPost]
        public async Task<ActionResult<EnvioDTO>> CrearEnvio(
            CrearEnvioDTO dto)
        {
            // Verificar que exista el cliente.
            bool clienteExiste = await _context.Clientes
                .AnyAsync(c => c.Id == dto.ClienteId);

            if (!clienteExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El cliente especificado no existe."
                });
            }

            // Verificar que exista el mensajero.
            var mensajero = await _context.Mensajeros
                .FirstOrDefaultAsync(m => m.Id == dto.MensajeroId);

            if (mensajero == null)
            {
                return BadRequest(new
                {
                    mensaje = "El mensajero especificado no existe."
                });
            }

            // Verificar que el mensajero esté disponible.
            if (mensajero.Estado != "Disponible")
            {
                return BadRequest(new
                {
                    mensaje = "El mensajero seleccionado no está disponible."
                });
            }

            // Verificar las direcciones.
            bool origenExiste = await _context.Direcciones
                .AnyAsync(d => d.Id == dto.DireccionOrigenId);

            bool destinoExiste = await _context.Direcciones
                .AnyAsync(d => d.Id == dto.DireccionDestinoId);

            if (!origenExiste || !destinoExiste)
            {
                return BadRequest(new
                {
                    mensaje = "La dirección de origen o destino no existe."
                });
            }

            // Validar la tarifa.
            if (dto.Tarifa < 0)
            {
                return BadRequest(new
                {
                    mensaje = "La tarifa no puede ser negativa."
                });
            }

            // Validar el estado inicial.
            if (dto.Estado != "Pendiente")
            {
                return BadRequest(new
                {
                    mensaje = "Un nuevo envío debe registrarse con estado Pendiente."
                });
            }

            // Crear el modelo que se guardará en la base de datos.
            var envio = new Envio
            {
                ClienteId = dto.ClienteId,
                MensajeroId = dto.MensajeroId,
                DireccionOrigenId = dto.DireccionOrigenId,
                DireccionDestinoId = dto.DireccionDestinoId,
                FechaRegistro = DateTime.Now,
                FechaEntrega = null,
                Tarifa = dto.Tarifa,
                Estado = "Pendiente"
            };

            _context.Envios.Add(envio);
            await _context.SaveChangesAsync();

            var respuesta = new EnvioDTO
            {
                Id = envio.Id,
                ClienteId = envio.ClienteId,
                MensajeroId = envio.MensajeroId,
                DireccionOrigenId = envio.DireccionOrigenId,
                DireccionDestinoId = envio.DireccionDestinoId,
                FechaRegistro = envio.FechaRegistro,
                FechaEntrega = envio.FechaEntrega,
                Tarifa = envio.Tarifa,
                Estado = envio.Estado
            };

            return CreatedAtAction(
                nameof(GetEnvio),
                new { id = envio.Id },
                respuesta
            );
        }

        // PUT: api/Envios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarEnvio(
            int id,
            CrearEnvioDTO dto)
        {
            var envio = await _context.Envios.FindAsync(id);

            if (envio == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el envío."
                });
            }

            // Verificar el cliente.
            bool clienteExiste = await _context.Clientes
                .AnyAsync(c => c.Id == dto.ClienteId);

            if (!clienteExiste)
            {
                return BadRequest(new
                {
                    mensaje = "El cliente especificado no existe."
                });
            }

            // Verificar el mensajero.
            var mensajero = await _context.Mensajeros
                .FirstOrDefaultAsync(m => m.Id == dto.MensajeroId);

            if (mensajero == null)
            {
                return BadRequest(new
                {
                    mensaje = "El mensajero especificado no existe."
                });
            }

            if (mensajero.Estado != "Disponible")
            {
                return BadRequest(new
                {
                    mensaje = "El mensajero seleccionado no está disponible."
                });
            }

            // Verificar las direcciones.
            bool origenExiste = await _context.Direcciones
                .AnyAsync(d => d.Id == dto.DireccionOrigenId);

            bool destinoExiste = await _context.Direcciones
                .AnyAsync(d => d.Id == dto.DireccionDestinoId);

            if (!origenExiste || !destinoExiste)
            {
                return BadRequest(new
                {
                    mensaje = "La dirección de origen o destino no existe."
                });
            }

            if (dto.Tarifa < 0)
            {
                return BadRequest(new
                {
                    mensaje = "La tarifa no puede ser negativa."
                });
            }

            string[] estadosPermitidos =
            {
                "Pendiente",
                "En tránsito",
                "Entregado",
                "Cancelado"
            };

            if (!estadosPermitidos.Contains(dto.Estado))
            {
                return BadRequest(new
                {
                    mensaje = "El estado del envío no es válido."
                });
            }

            envio.ClienteId = dto.ClienteId;
            envio.MensajeroId = dto.MensajeroId;
            envio.DireccionOrigenId = dto.DireccionOrigenId;
            envio.DireccionDestinoId = dto.DireccionDestinoId;
            envio.Tarifa = dto.Tarifa;
            envio.Estado = dto.Estado;

            if (dto.Estado == "Entregado")
            {
                envio.FechaEntrega ??= DateTime.Now;
            }
            else
            {
                envio.FechaEntrega = null;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Envios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarEnvio(int id)
        {
            var envio = await _context.Envios.FindAsync(id);

            if (envio == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró el envío."
                });
            }

            _context.Envios.Remove(envio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}