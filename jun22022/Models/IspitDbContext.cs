using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class IspitDbContext : DbContext
    {
        // DbSet...

        public DbSet<Kompanija> Kompanije { get; set; }
        public DbSet<Vozilo> Vozila { get; set; }

        public IspitDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
