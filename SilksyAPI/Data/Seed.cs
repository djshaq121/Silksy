using Microsoft.EntityFrameworkCore;
using SilksyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SilksyAPI.Data
{
    public class Seed
    {
        public static async Task SeedProducts(SilksyContext context)
        {
            if (await context.Products.AnyAsync())
                return;

            var productData = await System.IO.File.ReadAllTextAsync("SeedData/ProductSeed.json");
            var Products = JsonSerializer.Deserialize<List<Product>>(productData);

            foreach (var product in Products)
            {
                context.Products.Add(product);
            }

            await context.SaveChangesAsync();
        }
    }
}
