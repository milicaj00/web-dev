using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class IspitDbContext : DbContext
    {
        // DbSet...

        public DbSet<Prodavnica> Prodavnice { get; set; }
        public DbSet<Sastojak> Sastojci { get; set; }
        public DbSet<Spoj> Spoj { get; set; }

        public IspitDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
