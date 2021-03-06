using SilksyAPI.Dto;
using SilksyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI
{
    public class CartItemDto
    {
        public int ProductId { get; set; }

        public ProductDto Product { get; set; }

        public int Quantity { get; set; }
    }
}
