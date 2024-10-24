using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Northwin2.Entities
{
    public class Adresse
    {
        public Guid Id { get; set; }
        public string Rue { get; set; } = string.Empty;
        public string Ville { get; set; } = string.Empty;
        public string CodePostal { get; set; } = string.Empty;
        public string Pays { get; set; } = string.Empty;
        public string? Region { get; set; }
        public string? Tel { get; set; }
    }

    public class Employe
    {
        public int Id { get; set; }
        public Guid IdAdresse { get; set; }
        public int? IdManager { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string? Fonction { get; set; }
        public string? Civilite { get; set; }
        public DateTime? DateNaissance { get; set; }
        public DateTime? DateEmbauche { get; set; }
        public byte[]? Photo { get; set; }
        public string? Notes { get; set; }

        // Propriété de navigation
        public virtual Adresse Adresses { get; set; } = null!;
        public virtual List<Territoire> Territoires { get; set; } = new();
    }

    public class Affectation
    {
        public int IdEmploye { get; set; }
        public string IdTerritoire { get; set; } = string.Empty;
    }

    public class Territoire
    {
        [Key, MaxLength(20), Unicode(false)]
        public string Id { get; set; } = string.Empty;
        public int IdRegion { get; set; }
        public string Nom { get; set; } = string.Empty;

        public virtual Region Region { get; set; } = null!;
    }

    public class Region
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;

        // Propriété de navigation
        public virtual List<Territoire> Territoires { get; set; } = new();
    }
}
