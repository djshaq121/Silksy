using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Entities
{
    [Index(nameof(OrderNumber), IsUnique = true)]
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public int UserId { get; set; }
        public StoreUser User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public DateTime OrderDate { get; set; }

        public int BillingAddressId { get; set; }
        public Address BillingAddress { get; set; }

        public int ShippingAddressId { get; set; }

        public Address ShippingAddress { get; set; }

    }
}
