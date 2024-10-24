﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Northwin2.Data;

#nullable disable

namespace Northwin2.Data.Migrations
{
    [DbContext(typeof(ContexteNorthwind))]
    partial class ContexteNorthwindModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Northwin2.Entities.Adresse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodePostal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pays")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses", (string)null);
                });

            modelBuilder.Entity("Northwin2.Entities.Affectation", b =>
                {
                    b.Property<int>("IdEmploye")
                        .HasColumnType("int");

                    b.Property<string>("IdTerritoire")
                        .HasColumnType("varchar(20)");

                    b.HasKey("IdEmploye", "IdTerritoire");

                    b.HasIndex("IdTerritoire");

                    b.ToTable("Affectations", (string)null);
                });

            modelBuilder.Entity("Northwin2.Entities.Employe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Civilite")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTime?>("DateEmbauche")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateNaissance")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fonction")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<Guid>("IdAdresse")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("IdManager")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Notes")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("image");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("IdAdresse")
                        .IsUnique();

                    b.HasIndex("IdManager");

                    b.ToTable("Employes", (string)null);
                });

            modelBuilder.Entity("Northwin2.Entities.Region", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Regions", (string)null);
                });

            modelBuilder.Entity("Northwin2.Entities.Territoire", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("IdRegion")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.HasIndex("IdRegion");

                    b.ToTable("Territoires", (string)null);
                });

            modelBuilder.Entity("Northwin2.Entities.Affectation", b =>
                {
                    b.HasOne("Northwin2.Entities.Employe", null)
                        .WithMany()
                        .HasForeignKey("IdEmploye")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Northwin2.Entities.Territoire", null)
                        .WithMany()
                        .HasForeignKey("IdTerritoire")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Northwin2.Entities.Employe", b =>
                {
                    b.HasOne("Northwin2.Entities.Adresse", null)
                        .WithOne()
                        .HasForeignKey("Northwin2.Entities.Employe", "IdAdresse")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Northwin2.Entities.Employe", null)
                        .WithMany()
                        .HasForeignKey("IdManager");
                });

            modelBuilder.Entity("Northwin2.Entities.Territoire", b =>
                {
                    b.HasOne("Northwin2.Entities.Region", null)
                        .WithMany()
                        .HasForeignKey("IdRegion")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
