using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Dto
{
    public class CartDto
    {
       public ICollection<CartItemDto> CartItems { get; set; }
    }
}
