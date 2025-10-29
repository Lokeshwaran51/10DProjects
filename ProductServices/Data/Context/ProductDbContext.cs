using Microsoft.EntityFrameworkCore;

namespace ProductServices.Data.Context
{
    public class ProductDbContext:DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options) { }

        public DbSet<ProductServices.Data.Entity.Product> Products { get; set; }
    }
}
