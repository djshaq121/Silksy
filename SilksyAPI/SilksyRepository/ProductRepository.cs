using Microsoft.EntityFrameworkCore;
using SilksyAPI.Data;
using SilksyAPI.Entities;
using SilksyAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.SilksyRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly SilksyContext context;

        public ProductRepository(SilksyContext context)
        {
            this.context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await context.Products.SingleOrDefaultAsync(p => p.Id == productId);
        }
    }
}
