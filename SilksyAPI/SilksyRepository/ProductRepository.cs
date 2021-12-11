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
                .AsQueryable();

            if(productParams.BrandsId.HasValue)
                 query = query.Where(p => p.Brand.Id == productParams.BrandsId);
            
            if (productParams.CaetgoriesId.HasValue)
                query = query.Where(p => p.ProductCategories.Any(pc => pc.CategoryId == productParams.CaetgoriesId));

            switch (productParams.Sort)
            {
                case "pricedec":
                    query = query.OrderByDescending(p => p.Price);
                    break;
                case "priceasc":
                    query = query.OrderBy(p => p.Price);
                    break;
                default: 
                    query = query.OrderBy(p => p.Id);
                    break;
            };

            return await PagedList<ProductDto>.CreateAsync(query.ProjectTo<ProductDto>(mapper.ConfigurationProvider).AsNoTracking(),
                productParams.PageNumber, productParams.PageSize);
               
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

        public async Task<List<BrandWithCountDto>> GetBrandsAsync()
        {
            //var Brands = await context.Products
            //               .Join(context.Brands,
            //               p => p.Id,
            //               b => b.Id,

            //               )
            var BrandsWithCoundQuery = from p in context.Products
                         join b in context.Brands
                         on p.BrandId equals b.Id
                         group p.BrandId by new { p.BrandId, b.Name } into BrandGroup
                         select new BrandWithCountDto
                         {
                             Id = BrandGroup.Key.BrandId,
                             Name = BrandGroup.Key.Name,
                             Count = BrandGroup.Count()

                         };

            return await BrandsWithCoundQuery.ToListAsync();

            //return await context.Brands
            //     .ProjectTo<BrandDto>(mapper.ConfigurationProvider)
            //     .ToListAsync();
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            return await context.Categories
                .ProjectTo<CategoryDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
