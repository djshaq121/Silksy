using SilksyAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Helpers
{
    public class OrderCreateParams
    {
        public string PaymentIntentId { get; set; }

        public AddressDto ShippingAddress { get; set; }
    }
}
