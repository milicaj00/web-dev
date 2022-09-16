using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class IspitDbContext : DbContext
    {
        // DbSet...

        public DbSet<ProdukcijskaKuca> ProdukcijskaKuca { get; set; }
        public DbSet<Film> Filmovi { get ; set; }
        public DbSet<Kategorija> Kategorije { get ; set; }

        public IspitDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
