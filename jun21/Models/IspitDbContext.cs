using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class IspitDbContext : DbContext
    {
        // DbSet...

        public DbSet<Prodavnica> Prodavnice { get; set; }
        public DbSet<Proizvod> Proizvodi { get; set; }
        public DbSet<Sastojak> Sastojci { get; set; }
        public DbSet<ProdavnicaSastojak> ProdavnicaSastojak { get; set; }
        public DbSet<ProizvodSastojak> ProizvodSastojak { get; set; }

        public IspitDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
