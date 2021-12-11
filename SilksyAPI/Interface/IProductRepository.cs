using SilksyAPI.Dto;
using SilksyAPI.Entities;
using SilksyAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Interface
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int productId);
        Task<ProductDto> GetProductDtoByIdAsync(int productId);
        Task<PagedList<ProductDto>> GetProductsAsync(ProductParams productParams);

        Task<List<BrandWithCountDto>> GetBrandsAsync();

        Task<List<CategoryDto>> GetCategoriesAsync();

    }
}
