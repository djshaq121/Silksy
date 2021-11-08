using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilksyAPI.Data;
using SilksyAPI.Dto;
using SilksyAPI.Entities;
using SilksyAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        public IProductRepository productRepository { get; }

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts()
        {
            var products = await productRepository.GetProductsAsync();
            if (products.Count <= 0)
                return NoContent();

            return Ok(products);
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<List<BrandDto>>> GetBrands()
        {
            return Ok(await productRepository.GetBrandsAsync());
        }

        [HttpGet("Categories")]
        public async Task<ActionResult<List<CategoryDto>>> GetProductCategories()
        {
            return Ok(await productRepository.GetCategoriesAsync());
        }
    }
}
