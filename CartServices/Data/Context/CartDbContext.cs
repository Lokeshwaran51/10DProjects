using AmazonClone.API.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace CartServices.Data.Context
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options) : base(options) { }

        public DbSet<CartServices.Data.Entity.Cart> Carts { get; set; }
        public DbSet<CartServices.Data.Entity.CartItem> CartItems { get; set; }
        public DbSet<CartServices.Data.Entity.User> Users { get; set; }
        public DbSet<CartServices.Data.Entity.Product> Products { get; set; }
    }
}
