namespace WebApplication1.Models
{
    public class DossierPrelevement
    {
        public int Id { get; set; }

        // Utiliser une variable privée pour initialiser le statut par défaut
        private string _statut = "En attente";
        public string Statut
        {
            get => _statut;
            set => _statut = value;
        }

        public string Formulaire { get; set; }
        public List<Fichier> Fichiers { get; set; } = new List<Fichier>();

        // Relation avec le Client
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
