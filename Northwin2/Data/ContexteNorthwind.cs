using Microsoft.EntityFrameworkCore;
using Northwin2.Entities;

namespace Northwin2.Data
{
    public class ContexteNorthwind : DbContext
    {
        public ContexteNorthwind(DbContextOptions<ContexteNorthwind> options) : base(options)
        {
        }

        public virtual DbSet<Adresse> Addresses { get; set; }
        public virtual DbSet<Employe> Employes { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Territoire> Territoires { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employe>(entity =>
            {
                entity.ToTable("Employes");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Prenom).HasMaxLength(40);
                entity.Property(e => e.Nom).HasMaxLength(40);
                entity.Property(e => e.Notes).HasMaxLength(1000);
                entity.Property(e => e.Photo).HasColumnType("image");
                entity.Property(e => e.Fonction).HasMaxLength(40);
                entity.Property(e => e.Civilite).HasMaxLength(40);

                entity.HasOne<Employe>().WithMany().HasForeignKey(d => d.IdManager);
                entity.HasOne(e => e.Adresses).WithOne().HasForeignKey<Employe>(d => d.IdAdresse).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Affectation>(entity =>
            {
                entity.ToTable("Affectations");
                entity.HasKey(e => new { e.IdEmploye, e.IdTerritoire });
                //entity.Property(e => e.IdTerritoire).HasMaxLength(20).IsUnicode(false);

                entity.HasOne<Employe>().WithMany().HasForeignKey(d => d.IdEmploye);
                entity.HasOne<Territoire>().WithMany().HasForeignKey(d => d.IdTerritoire);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Regions");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Nom).HasMaxLength(40);
                //entity.HasMany<Territoire>().WithOne().HasForeignKey(d => d.IdRegion).OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Territoire>(entity =>
            {
                entity.ToTable("Territoires");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.Nom).HasMaxLength(40);
                entity.HasOne(t => t.Region).WithMany(r => r.Territoires).HasForeignKey(d => d.IdRegion).OnDelete(DeleteBehavior.NoAction);

                // Crée la relation N-N avec Employé en utilisant Affectation comme entité d'association
                entity.HasMany<Employe>().WithMany(e => e.Territoires).UsingEntity<Affectation>(
                    l => l.HasOne<Employe>().WithMany().HasForeignKey(a => a.IdEmploye),
                    r => r.HasOne<Territoire>().WithMany().HasForeignKey(a => a.IdTerritoire)
                );
            });
        }
    }
}
