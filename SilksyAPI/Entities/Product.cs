using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
