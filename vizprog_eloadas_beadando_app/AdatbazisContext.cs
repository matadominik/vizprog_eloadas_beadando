using Microsoft.EntityFrameworkCore;

namespace vizprog_eloadas_beadando_app
{
    public class AdatbazisContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=.\\SQLEXPRESS;Initial Catalog=naplo;Integrated Security=True;TrustServerCertificate=True;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diak>().ToTable("diakok");
            modelBuilder.Entity<Diak>().HasKey(d => d.id);

            modelBuilder.Entity<Jegy>().ToTable("jegyek");
            modelBuilder.Entity<Jegy>().HasKey(j => j.id);

            modelBuilder.Entity<Targy>().ToTable("targyak");
            modelBuilder.Entity<Targy>().HasKey(t => t.id);
        }

        public DbSet<Diak> Diakok => Set<Diak>();
        public DbSet<Jegy> Jegyek => Set<Jegy>();
        public DbSet<Targy> Targyak => Set<Targy>();
    }
}