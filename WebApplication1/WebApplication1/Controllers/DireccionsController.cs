using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using SistemaMensajeriaAPI.Data;
namespace WebApplication1.Controllers;


[Route("api/[controller]")]
[ApiController]
public class DireccionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public DireccionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Direccion
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Direccion>>> GetDireccion()
    {
        return await _context.Direcciones.ToListAsync();
    }

    // GET: api/Direccion/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Direccion>> GetDireccion(int id)
    {
        var direccion = await _context.Direcciones.FindAsync(id);

        if (direccion == null)
        {
            return NotFound();
        }

        return direccion;
    }

    // PUT: api/Direccion/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDireccion(int id, Direccion direccion)
    {
        if (id != direccion.Id)
        {
            return BadRequest();
        }

        _context.Entry(direccion).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DireccionExists(id))
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

    // POST: api/Direccion
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Direccion>> PostDireccion(Direccion direccion)
    {
        _context.Direcciones.Add(direccion);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDireccion", new { id = direccion.Id }, direccion);
    }

    // DELETE: api/Direccion/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDireccion(int id)
    {
        var direccion = await _context.Direcciones.FindAsync(id);
        if (direccion == null)
        {
            return NotFound();
        }

        _context.Direcciones.Remove(direccion);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DireccionExists(int id)
    {
        return _context.Direcciones.Any(e => e.Id == id);
    }
}


