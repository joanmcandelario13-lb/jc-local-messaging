using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using SistemaMensajeriaAPI.Data;
namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MensajeroesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public MensajeroesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Mensajero
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Mensajero>>> GetMensajero()
    {
        return await _context.Mensajeros.ToListAsync();
    }

    // GET: api/Mensajero/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Mensajero>> GetMensajero(int id)
    {
        var mensajero = await _context.Mensajeros.FindAsync(id);

        if (mensajero == null)
        {
            return NotFound();
        }

        return mensajero;
    }

    // PUT: api/Mensajero/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMensajero(int id, Mensajero mensajero)
    {
        if (id != mensajero.Id)
        {
            return BadRequest();
        }

        _context.Entry(mensajero).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MensajeroExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Mensajero
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Mensajero>> PostMensajero(Mensajero mensajero)
    {
        _context.Mensajeros.Add(mensajero);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMensajero", new { id = mensajero.Id }, mensajero);
    }

    // DELETE: api/Mensajero/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMensajero(int id)
    {
        var mensajero = await _context.Mensajeros.FindAsync(id);
        if (mensajero == null)
        {
            return NotFound();
        }

        _context.Mensajeros.Remove(mensajero);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MensajeroExists(int id)
    {
        return _context.Mensajeros.Any(e => e.Id == id);
    }
}
