using Microsoft.AspNetCore.Mvc;
using SilksyAPI.Data;
using SilksyAPI.Entities;
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
        private readonly SilksyContext context;

        public ProductController(SilksyContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            var products = context.Products.ToList();
            if (products.Count <= 0)
                return NoContent();

            return Ok(products);
        }
    }
}
