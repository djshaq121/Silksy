using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SilksyAPI.Data;
using SilksyAPI.Dto;
using SilksyAPI.Entities;
using SilksyAPI.Helpers;
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
        private readonly IMapper mapper;

        public ProductRepository(SilksyContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PagedList<ProductDto>> GetProductsAsync(ProductParams productParams)
        {
            var query = context.Products
                .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<ProductDto>.CreateAsync(query, productParams.PageNumber, productParams.PageSize);
               
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await context.Products.SingleOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<ProductDto> GetProductDtoByIdAsync(int productId)
        {
            return await context.Products
                .Where(p => p.Id == productId)
                .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<List<BrandDto>> GetBrandsAsync()
        {
            return await context.Brands
                 .ProjectTo<BrandDto>(mapper.ConfigurationProvider)
                 .ToListAsync();
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            return await context.Categories
                .ProjectTo<CategoryDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
