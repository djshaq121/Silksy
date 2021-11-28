using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Dto
{
    public class OrderItemsDto
    {
        public ProductDto Product { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
