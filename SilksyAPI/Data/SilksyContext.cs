using Microsoft.EntityFrameworkCore;
using SilksyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Data
{
    public class SilksyContext : DbContext
    {
        public SilksyContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            builder.Entity<ProductCategory>()
               .HasOne(pc => pc.Product)
               .WithMany(p => p.ProductCategories)
               .HasForeignKey(pc => pc.ProductId);

            builder.Entity<ProductCategory>()
               .HasOne(pc => pc.Category)
               .WithMany(c => c.ProductCategories)
               .HasForeignKey(pc => pc.CategoryId);

            //builder.Entity<Product>()
            //    .HasOne(p => p.Brand)
            //    .WithOne()
            //    .HasForeignKey<Product>(p => p.BrandId);
            // INFO - The EF Team are removing the need for a join entity from .NET Core 5
            //builder.Entity<Cart>()
            //    .HasMany(cart => cart.CartItems)
            //    .WithOne(ci => ci.Cart)
            //    .IsRequired();
        }

        public DbSet<StoreUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
