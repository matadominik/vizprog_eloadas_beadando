using Microsoft.EntityFrameworkCore;

namespace vizprog_eloadas_beadando_app
{
    public class AdatbazisContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=naplo;" +
                "Integrated Security=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Diak>().HasKey(d => d.id);
            modelBuilder.Entity<Jegy>().HasKey(j => j.id);
            modelBuilder.Entity<Targy>().HasKey(t => t.id);

           
            modelBuilder.Entity<Diak>()
                .Property(d => d.id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Targy>()
                .Property(t => t.id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Jegy>()
                .Property(j => j.id)
                .ValueGeneratedOnAdd();
        }

        public DbSet<Diak> Diakok { get; set; }
        public DbSet<Jegy> Jegyek { get; set; }
        public DbSet<Targy> Targyak { get; set; }
    }
}