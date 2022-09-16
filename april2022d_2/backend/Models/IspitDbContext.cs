using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class IspitDbContext : DbContext
    {
        // DbSet...

        public DbSet<Prodavnica> Prodavnice { get; set; }
        public DbSet<Spoj> Spoj { get; set; }
        public DbSet<Komponenta> Komponente { get; set; }
        public DbSet<Tip> Tipovi { get; set; }
        public DbSet<Brend> Brendovi { get; set; }

        public IspitDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
