using Microsoft.EntityFrameworkCore;
using Northwin2.Data;
using Northwin2.Entities;

namespace Northwin2.Services
{
    public interface IEmployeService
    {
        Task<List<Employe>> GetEmployes(string? rechercheNom, DateTime? dateEmbaucheMax);
        Task<Employe?> GetEmployeById(int id);
        Task<Region?> GetRegion(int id);
        Task<Employe> AddEmpoye(Employe empl);
    }
    public class EmployeServices : IEmployeService
    {
        private readonly ContexteNorthwind _context;

        public EmployeServices(ContexteNorthwind context)
        {
            _context = context;
        }

        public async Task<List<Employe>> GetEmployes(string? rechercheNom, DateTime? dateEmbaucheMax)
        {
            // DbSet<Employe> req = _context.Employes;
            // IQueryable<Employe> req = from e in _context.Employes select e;
            IQueryable<Employe> req = from e in _context.Employes
                                      where (
                                      (rechercheNom == null || e.Nom.Contains(rechercheNom))
                                      && 
                                      (dateEmbaucheMax == null || e.DateEmbauche == null || e.DateEmbauche <= dateEmbaucheMax)
                                      )
                                      select new Employe
                                      {
                                          Id = e.Id,
                                          Civilite = e.Civilite,
                                          Nom = e.Nom,
                                          Prenom = e.Prenom,
                                          Fonction = e.Fonction,
                                          DateEmbauche = e.DateEmbauche,
                                      };

            // Tri par date d'embauche décroissante
            if (dateEmbaucheMax != null) 
            { 
                req = req.OrderByDescending(e => e.DateEmbauche);
            }
            else
            {
                req = req.OrderBy(e => e.Nom).ThenBy(e => e.Prenom);
            }

            return await req.ToListAsync();
        }
        
        public async Task<Employe?> GetEmployeById(int id)
        {
            IQueryable<Employe> req = from e in _context.Employes
                                      .Include(e => e.Adresses)
                                      .Include(e => e.Territoires) 
                                      .ThenInclude(t => t.Region)
                                      where (e.Id == id) 
                                      select e;

            return await req.FirstOrDefaultAsync();
            // return await _context.Employes.FindAsync(id);
        }

        // Récupère une région et ses territoires
        public async Task<Region?> GetRegion(int id)
        {
            IQueryable<Region> req = from r in _context.Regions.Include(r => r.Territoires) 
                                     where (r.Id == id) 
                                     select r;

            return await req.FirstOrDefaultAsync();
        }

        // Ajoute un employé
        public async Task<Employe> AddEmpoye(Employe empl)
        {
            // Ajoute l'employé dans le DbSet
            _context.Employes.Add(empl);

            // Enregistre l'employé dans la base et affecte son Id
            await _context.SaveChangesAsync();

            return empl; // Renvoie l'employé avec son Id renseigné
        }
    }
}
