using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class IspitDbContext : DbContext
    {
        // DbSet...

        public DbSet<Prodavnica> Prodavnice { get; set; }
        public DbSet<Artikal> Artikli { get; set; }
        public DbSet<Brend> Brendovi { get; set; }
        public DbSet<Spoj> ProdavnicaArtikal { get; set; }



        public IspitDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
