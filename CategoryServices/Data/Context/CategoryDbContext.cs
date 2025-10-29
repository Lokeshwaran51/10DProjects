using Microsoft.EntityFrameworkCore;

namespace CategoryServices.Data.Context
{
    public class CategoryDbContext:DbContext
    {
        public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options) { }

        public DbSet<CategoryServices.Data.Entity.Category> Categories { get; set; }
        public DbSet<CategoryServices.Data.Entity.SubCategory> SubCategories { get; set; }
    }
}
