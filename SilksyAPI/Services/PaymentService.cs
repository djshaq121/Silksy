using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SilksyAPI.Data;
using SilksyAPI.Entities;
using SilksyAPI.Interface;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration config;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public PaymentService(IConfiguration config, IShoppingCartRepository shoppingCartRepository)
        {
            this.config = config;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public int CalculateOrderAmountFromCart(Cart cart)
        {
            if (cart == null)
                return 0;

            int sum = 0;
            foreach (var item in cart.CartItems)
            {
                int unitAmount = (int)Decimal.Multiply(item.Product.Price, 100);
                sum += unitAmount * item.Quantity;
            }

            return sum;
        }

        public List<SessionLineItemOptions> CreateItemsFromCart(Cart cart)
        {
            var SessionLineItems = new List<SessionLineItemOptions>();
            foreach (var item in cart.CartItems)
            {
                var PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)Decimal.Multiply(item.Product.Price, 100),
                    Currency = "gbp",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Product.Name,
                        Description = item.Product.Description
                    },

                };

                SessionLineItems.Add(new SessionLineItemOptions
                {
                    PriceData = PriceData,
                    Quantity = item.Quantity
                });

            }

            return SessionLineItems;
        }

        public async Task<ActionResult> HandlePaymentIntentFailedAsync(string paymentIntentId)
        {
            throw new NotImplementedException();
        }

        public async Task<ActionResult> HandlePaymentIntentSucceededAsync(string paymentIntentId)
        {
            throw new NotImplementedException();
        }
    }
}
