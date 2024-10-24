namespace Northwin2.Entities
{
    public class Commandes
    {
        public int Id { get; set; }
        public Guid IdAdresse { get; set; }
        public string IdClient { get; set; } = "";
        public int IdEmploye { get; set; }
        public int IdLivreur { get; set; }
        public DateTime DateCommande { get; set; }
        public DateTime? DateLivMaxi { get; set; }
        public DateTime? DateLivraison { get; set; }
        public decimal? FraisLivraison { get; set; }

        // Propriétés de navigation
        public virtual List<LigneCommande> Lignes { get; set; } = new();
        public virtual Employe Employe { get; set; } = new();
        public virtual Adresse Adresse { get; set; } = new();
        //public virtual Livreur Livreur { get; set; } = new();
    }

    public class LigneCommande
    {
        public int IdCommande { get; set; }
        public int IdProduit { get; set; }

        public decimal PU { get; set; }
        public short Quantite { get; set; }
        public float TauxReduc { get; set; }
        public virtual Produit Produit { get; set; } = new();
    }
}
