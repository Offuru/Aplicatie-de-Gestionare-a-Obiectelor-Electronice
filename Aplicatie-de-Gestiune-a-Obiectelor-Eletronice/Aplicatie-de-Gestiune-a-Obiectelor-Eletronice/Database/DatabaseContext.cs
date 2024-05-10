using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Models;
using Microsoft.EntityFrameworkCore;

namespace Aplicatie_de_Gestiune_a_Obiectelor_Eletronice.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ElectronicObject> ElectronicObjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql("Host=localhost;Database=casare_obiecte;Username=postgres;Password=1q2w3e");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ElectronicObject>()
                .HasKey(e => e.Id);
        }
    }
}
