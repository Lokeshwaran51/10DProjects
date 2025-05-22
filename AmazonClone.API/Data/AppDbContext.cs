using AmazonClone.API.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static AmazonClone.API.Data.Entity.ViewModelAPI;


namespace AmazonClone.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
       /* public DbSet<Cart> Carts {  get; set; }
        public DbSet<CartItem> CartItems { get; set; }*/


        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<ViewModelAPI.Product>()
        .Property(p => p.Price)
        .HasPrecision(18, 2);

    modelBuilder.Entity<ViewModelAPI.Product>()
        .HasOne(p => p.Category)
        .WithMany()
        .HasForeignKey(p => p.CategoryId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<ViewModelAPI.Product>()
        .HasOne(p => p.SubCategory)
        .WithMany()
        .HasForeignKey(p => p.SubCategoryId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<ViewModelAPI.SubCategory>()
        .HasOne(sc => sc.Category)
        .WithMany(c => c.SubCategories)
        .HasForeignKey(sc => sc.CategoryId)
        .OnDelete(DeleteBehavior.Restrict);

    /*modelBuilder.Entity<ViewModelAPI.Cart>()
        .HasOne(c => c.User)
        .WithMany()
        .HasForeignKey(c => c.UserId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<ViewModelAPI.CartItem>()
        .HasOne(ci => ci.Cart)
        .WithMany(c => c.CartItems)
        .HasForeignKey(ci => ci.CartId)
        .OnDelete(DeleteBehavior.Restrict);

    modelBuilder.Entity<ViewModelAPI.CartItem>()
        .HasOne(ci => ci.Product)
        .WithMany()
        .HasForeignKey(ci => ci.ProductId)
        .OnDelete(DeleteBehavior.Restrict);*/
}



    }
}