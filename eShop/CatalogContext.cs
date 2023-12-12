using Microsoft.EntityFrameworkCore;

namespace eShop
{
    public class CatalogContext:DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<CatalogItem> CatalogItems { get; set; }
    }
}
