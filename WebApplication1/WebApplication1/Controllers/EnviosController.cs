using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using SistemaMensajeriaAPI.Data;
namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnviosController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public EnviosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Envio
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Envio>>> GetEnvio()
    {
        return await _context.Envios.ToListAsync();
    }

    // GET: api/Envio/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Envio>> GetEnvio(int id)
    {
        var envio = await _context.Envios.FindAsync(id);

        if (envio == null)
        {
            return NotFound();
        }

        return envio;
    }

    // PUT: api/Envio/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEnvio(int id, Envio envio)
    {
        if (id != envio.Id)
        {
            return BadRequest();
        }

        _context.Entry(envio).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EnvioExists(id))
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

    // POST: api/Envio
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Envio>> PostEnvio(Envio envio)
    {
        _context.Envios.Add(envio);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetEnvio", new { id = envio.Id }, envio);
    }

    // DELETE: api/Envio/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEnvio(int id)
    {
        var envio = await _context.Envios.FindAsync(id);
        if (envio == null)
        {
            return NotFound();
        }

        _context.Envios.Remove(envio);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EnvioExists(int id)
    {
        return _context.Envios.Any(e => e.Id == id);
    }
}
