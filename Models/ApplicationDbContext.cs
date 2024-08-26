using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {

        // Définir les DbSets pour vos entités
        public DbSet<Client> Clients { get; set; }
        public DbSet<DossierPrelevement> DossiersPrelevement { get; set; }

        public DbSet<Fichier> Fichiers { get; set; }

        // Configurer les relations, les clés étrangères, etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fichier>()
                .HasOne(f => f.DossierPrelevement)
                .WithMany(d => d.Fichiers)
                .HasForeignKey(f => f.DossierPrelevementId)
                .OnDelete(DeleteBehavior.Cascade);
            ;

            base.OnModelCreating(modelBuilder);
        }
    }
}
