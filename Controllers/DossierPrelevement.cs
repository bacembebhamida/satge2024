using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Authorize] // Tous les utilisateurs doivent être authentifiés
    [Route("api/[controller]")]
    [ApiController]
    public class DossierPrelevementController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DossierPrelevementController(ApplicationDbContext context) => _context = context;

        // Soumettre un dossier par un utilisateur
        [HttpPost("submit")]
        [Authorize(Roles = "User")] // Seuls les utilisateurs peuvent soumettre des dossiers
        public async Task<IActionResult> SubmitDossier([FromBody] DossierPrelevement dossier)
        {
            if (ModelState.IsValid)
            {
                dossier.Statut = "En attente";
                _context.DossiersPrelevement.Add(dossier);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Dossier soumis avec succès" });
            }

            return BadRequest(ModelState);
        }

        // L'administrateur peut accepter ou refuser un dossier
        [HttpPost("review/{id}")]
        [Authorize(Roles = "Admin")] // Seuls les administrateurs peuvent réviser les dossiers
        public async Task<IActionResult> ReviewDossier(int id, [FromBody] string decision)
        {
            var dossier = await _context.DossiersPrelevement.FindAsync(id);

            if (dossier == null)
            {
                return NotFound();
            }

            if (decision == "accepter")
            {
                dossier.Statut = "En cours de traitement";
            }
            else if (decision == "refuser")
            {
                dossier.Statut = "Refusé";
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = $"Le dossier a été {dossier.Statut}" });
        }

        // Modifier un dossier par un administrateur
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin")] // Seuls les administrateurs peuvent modifier les dossiers
        public async Task<IActionResult> UpdateDossier(int id, [FromBody] DossierPrelevement updatedDossier)
        {
            var dossier = await _context.DossiersPrelevement.FindAsync(id);

            if (dossier == null)
            {
                return NotFound();
            }

            // Mise à jour du formulaire et des fichiers si nécessaire
            dossier.Formulaire = updatedDossier.Formulaire;
            dossier.Fichiers = updatedDossier.Fichiers;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Dossier modifié avec succès" });
        }
    }
}
