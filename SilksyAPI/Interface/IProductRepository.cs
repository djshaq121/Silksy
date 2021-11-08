using SilksyAPI.Dto;
using SilksyAPI.Entities;
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
        Task<List<ProductDto>> GetProductsAsync();

        Task<List<BrandDto>> GetBrandsAsync();

        Task<List<CategoryDto>> GetCategoriesAsync();

    }
}
