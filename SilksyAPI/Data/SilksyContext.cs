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

        public DbSet<StoreUser> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
