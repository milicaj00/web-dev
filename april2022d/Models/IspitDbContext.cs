using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class IspitDbContext : DbContext
    {
        // DbSet...

        public DbSet<Prodavnica> Prodavnice { get; set; }
        public DbSet<Spoj> Spojevi { get; set; }
        public DbSet<Komponenta> Komponente { get; set; }
        public DbSet<Brend> Brend { get; set; }
        public DbSet<Tip> Tip { get; set; }


        public IspitDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
