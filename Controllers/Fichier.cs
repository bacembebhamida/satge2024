using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FichiersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FichiersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Fichiers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fichier>>> GetFichiers()
        {
            return await _context.Fichiers.ToListAsync();
        }

        // GET: api/Fichiers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fichier>> GetFichier(int id)
        {
            var fichier = await _context.Fichiers.FindAsync(id);

            if (fichier == null)
            {
                return NotFound();
            }

            return fichier;
        }

        // POST: api/Fichiers
        [HttpPost]
        public async Task<ActionResult<Fichier>> PostFichier(Fichier fichier)
        {
            _context.Fichiers.Add(fichier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFichier), new { id = fichier.Id }, fichier);
        }

        // PUT: api/Fichiers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFichier(int id, Fichier fichier)
        {
            if (id != fichier.Id)
            {
                return BadRequest();
            }

            _context.Entry(fichier).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FichierExists(id))
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

        // DELETE: api/Fichiers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFichier(int id)
        {
            var fichier = await _context.Fichiers.FindAsync(id);
            if (fichier == null)
            {
                return NotFound();
            }

            _context.Fichiers.Remove(fichier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FichierExists(int id)
        {
            return _context.Fichiers.Any(e => e.Id == id);
        }
    }
}
