namespace WebApplication1.Models
{
    public class Fichier
    {
        public int Id { get; set; }
        public required string NomFichier { get; set; }
        public string CheminFichier { get; set; }
        public int DossierPrelevementId { get; set; }
        public DossierPrelevement DossierPrelevement { get; set; }= new();
    }
}
