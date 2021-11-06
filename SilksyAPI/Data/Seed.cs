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

        public static async Task SeedBrands(SilksyContext context)
        {
            if (await context.Brands.AnyAsync())
                return;

            var brandData = await System.IO.File.ReadAllTextAsync("SeedData/BrandSeed.json");
            var Brand = JsonSerializer.Deserialize<List<Brand>>(brandData);


            foreach (var brand in Brand)
            {
                context.Brands.Add(brand);
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedCategories(SilksyContext context)
        {
            if (await context.Categories.AnyAsync())
                return;

            var categoryData = await System.IO.File.ReadAllTextAsync("SeedData/CategorySeed.json");
            var Category = JsonSerializer.Deserialize<List<Category>>(categoryData);


            foreach (var category in Category)
            {
                context.Categories.Add(category);
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedProductCategories(SilksyContext context)
        {
            if (await context.ProductCategories.AnyAsync())
                return;

            var productCategoryData = await System.IO.File.ReadAllTextAsync("SeedData/ProductCategorySeed.json");
            var ProductCategory = JsonSerializer.Deserialize<List<ProductCategory>>(productCategoryData);

            foreach (var productCategory in ProductCategory)
            {
                context.ProductCategories.Add(productCategory);
            }

            await context.SaveChangesAsync();
        }
    }
}
