using Microsoft.AspNetCore.Mvc;
using SilksyAPI.Entities;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Interface
{
    public interface IPaymentService
    {
        public int CalculateOrderAmountFromCart(Cart cart);

        public List<SessionLineItemOptions> CreateItemsFromCart(Cart cart);

        public Task<ActionResult> HandlePaymentIntentSucceededAsync(string paymentIntentId);

        public Task<ActionResult> HandlePaymentIntentFailedAsync(string paymentIntentId);
    }
}
