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

            //builder.Entity<Cart>()
            //    .HasMany(cart => cart.CartItems)
            //    .WithOne(ci => ci.Cart)
            //    .IsRequired();
        }

        public DbSet<StoreUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
