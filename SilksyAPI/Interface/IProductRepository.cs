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

        Task<List<Product>> GetAllProductsAsync();
    }
}
