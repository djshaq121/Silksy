using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Entities
{
    //[Index(nameof(PaymentIntentId), IsUnique = true)] - For testing purpose, i disabled this
    public class Order
    {
        public Order()
        {

        }
        public Order(StoreUser user, ICollection<OrderItem> orderItems, Address shippingAddress,  string paymentIntentId)
        {
            User = user;
            OrderItems = orderItems;
            ShippingAddress = shippingAddress;
            BillingAddress = shippingAddress;
            PaymentIntentId = paymentIntentId;
        }

        public int Id { get; set; }
        public string PaymentIntentId { get; set; }
        public int UserId { get; set; }
        public StoreUser User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public int BillingAddressId { get; set; }
        public Address BillingAddress { get; set; }

        public int ShippingAddressId { get; set; }

        public Address ShippingAddress { get; set; }

        public decimal TotalPrice { get { return OrderItems.Sum(ot => ot.UnitPrice * ot.Quantity);  } }

    }
}
