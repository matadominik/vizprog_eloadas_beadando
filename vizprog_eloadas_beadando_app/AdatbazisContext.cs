using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace vizprog_eloadas_beadando_app
{
    public class AdatbazisContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=naplo;Integrated Security=True;TrustServerCertificate=True;");
        }

        public DbSet<Diak> Diakok { get; set; }
        public DbSet<Jegy> Jegyek { get; set; }
        public DbSet<Targy> Targyak { get; set; }
    }
}
