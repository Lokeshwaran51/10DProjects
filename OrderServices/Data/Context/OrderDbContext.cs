using Microsoft.EntityFrameworkCore;

namespace OrderServices.Data.Context
{
    public class OrderDbContext:DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<OrderServices.Data.Entity.Product> Products { get; set; }
        public DbSet<OrderServices.Data.Entity.Order> Orders { get; set; }
    }

}
