using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Client
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _context.Clients.ToListAsync();
            return Ok(clients);
        }

        // GET: api/Client/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            if (User.IsInRole("User") && client.Email != User.Identity.Name)
            {
                return Forbid();
            }

            return Ok(client);
        }

        // POST: api/Client
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }

        // PUT: api/Client/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] Client updatedClient)
        {
            if (id != updatedClient.Id)
            {
                return BadRequest("Client ID mismatch");
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            if (User.IsInRole("User") && client.Email != User.Identity.Name)
            {
                return Forbid();
            }

            // Update client properties
            client.Nom = updatedClient.Nom;
            client.Prenom = updatedClient.Prenom;
            client.Email = updatedClient.Email;
            client.Telephone = updatedClient.Telephone;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Client/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
