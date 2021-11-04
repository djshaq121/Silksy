using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilksyAPI.Data;
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
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await productRepository.GetAllProductsAsync();
            if (products.Count <= 0)
                return NoContent();

            return Ok(products);
        }
    }
}
