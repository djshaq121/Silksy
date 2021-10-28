using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SilksyAPI.Data;
using SilksyAPI.Dto;
using SilksyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SilksyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly SilksyContext context;
        private readonly IMapper mapper;

        public ShoppingCartController(SilksyContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpPost("AddProduct")]
        public async Task<ActionResult> AddItemToShoppingCart(CartItemDto cartItemDto)
        {
            // We need to authincate here
            if (cartItemDto.Quantity <= 0)
                BadRequest("No cart data sent");

            var product = await context.Products.FindAsync(cartItemDto.ProductId);
            if(product == null)
                BadRequest("Product not found");

            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await context.Users.SingleOrDefaultAsync(u => u.UserName == username);

            //check if we have an exist cart
            var cart = await context.Carts.Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .SingleOrDefaultAsync(cart => cart.UserId == user.Id);

            if(cart == null)
            {
                cart = new Cart()
                { 
                    User = user,
                    CartItems = new List<CartItem>()
                };

                cart.CartItems.Add(new CartItem
                {
                    Product = product,
                    Quantity = cartItemDto.Quantity
                });

                context.Carts.Add(cart);

            } 
            else
            {
                if (cart.CartItems == null || !cart.CartItems.Any())
                    return BadRequest("Internal Server Error");

                var cartItemExistingProduct = cart.CartItems.First(ci => ci.Product == product);

                if(cartItemExistingProduct != null)
                    cartItemExistingProduct.Quantity += cartItemDto.Quantity;
                else
                {
                    cart.CartItems.Add(new CartItem
                    {
                        Product = product,
                        Quantity = cartItemDto.Quantity
                    });
                }
            }

            var result = await context.SaveChangesAsync();
            if(result > 0)
                return NoContent();

            return BadRequest("Failed to save shopping cart");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CartDto>> GetShoppingCart()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserName == username);

            var cart = await context.Carts.Include(c => c.CartItems)
               .ThenInclude(ci => ci.Product)
               .SingleOrDefaultAsync(cart => cart.UserId == user.Id);

            if (cart == null)
                return Ok();

            //Use auto mapper from cart to cartDto

            var cartDto = mapper.Map<CartDto>(cart);

            if (cartDto == null)
                return BadRequest("Failed to get cart");

            return Ok(cartDto);
        }

        [Authorize]
        [HttpDelete("RemoveProduct")]
        public Task<ActionResult> RemoveProductFromCart()
        {
            throw new NotImplementedException("This will be implemented soon");
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
