using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public ICollection<OrderItemsDto> OrderItems { get; set; }

        public AddressDto ShippingAddress { get; set; }
        public DateTimeOffset OrderDate { get; set; }

        public decimal TotalPrice { get; set; }

    }
}
