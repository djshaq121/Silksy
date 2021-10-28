using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilksyAPI.Data;
using SilksyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly SilksyContext context;

        public ShoppingCartController(SilksyContext context)
        {
            this.context = context;
        }

        [Authorize]
        [HttpPost("AddProduct")]
        public async Task<ActionResult> AddItemToShoppingCart(CartItemDto cartItemDto)
        {
            // We need to authincate here
            if (cartItemDto.Quantity <= 0)
                BadRequest("No cart data sent");

            var cart = new Cart();

            //// check if they exist in product table
            //foreach (var cartItemRequest in cartItemsRequest)
            //{
            //    var product = await context.Products.FindAsync(cartItemRequest.ProductId);
            //    if (product != null || cartItemRequest.Quantity > 0)
            //    {
            //        var cartItem = new CartItem
            //        {
            //            Product = product,
            //            Quantity = cartItemRequest.Quantity,
            //            Cart = cart
            //        };

            //        cart.CartItems.Add(cartItem);
            //    }
            //}

            //if (!cart.CartItems.Any())
            //    return BadRequest("Failed to add items to shopping cart");

            return NoContent();
            //
            //cart.User = user

        }

        [Authorize]
        [HttpPost("AddProducts")]
        public async Task<ActionResult> AddItemsToShoppingCart(List<CartItemDto> cartItemsDto)
        {
            // We need to authincate here
            if (!cartItemsDto.Any())
                BadRequest("No cart data sent");

            var cart = new Cart();

            // check if they exist in product table
            foreach (var cartItemRequest in cartItemsDto)
            {
                var product = await context.Products.FindAsync(cartItemRequest.ProductId);
                if (product != null || cartItemRequest.Quantity > 0)
                {
                    var cartItem = new CartItem
                    {
                        Product = product,
                        Quantity = cartItemRequest.Quantity,
                        Cart = cart
                    };

                    cart.CartItems.Add(cartItem);
                }
            }

            if (!cart.CartItems.Any())
                return BadRequest("Failed to add items to shopping cart");

            return NoContent();
            //
            //cart.User = user

        }
    }
}
