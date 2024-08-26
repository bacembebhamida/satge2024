namespace WebApplication1.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }

        // Propriété pour le rôle (User ou Admin)
        private string _role = "User";
        public string Role
        {
            get => _role;
            set => _role = value;
        }

        // Relation avec les dossiers de prélèvement
        public List<DossierPrelevement> DossiersPrelevement { get; set; } = new List<DossierPrelevement>();
    }
}
