using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SilksyAPI.Data;
using SilksyAPI.Dto;
using SilksyAPI.Entities;
using SilksyAPI.Interface;
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
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IUserRepository userRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, 
            IUserRepository userRepository, IProductRepository productRepository,  IMapper mapper)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.userRepository = userRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpPost("UpdateCart")]
        public async Task<ActionResult<CartDto>> UpdateShoppingCart(CartDto cartDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await userRepository.GetUserByUsernameAsync(username);

            var cart = new Cart();
            foreach (var ci in cartDto.CartItems)
            {
                // Maybe remove item if the quantity i zero
                if (ci.Quantity <= 0 || ci.Quantity > 10)
                    ci.Quantity = 1;

                ProductDto verifiedProduct = await productRepository.GetProductDtoByIdAsync(ci.ProductId);
                if (verifiedProduct == null)
                    return BadRequest("Invalid Product");

                CartItem cartItem = new CartItem
                {
                    ProductId = verifiedProduct.Id,
                    Quantity = ci.Quantity
                };

                cart.CartItems.Add(cartItem);
                ci.Product = verifiedProduct;
            }

            var existingCart = await shoppingCartRepository.GetCartByUserIdAsync(user.Id);

            if (existingCart != null)
                existingCart.CartItems = cart.CartItems;
            else
            {
                cart.User = user;
                shoppingCartRepository.AddCart(cart);
            }

            var result = await shoppingCartRepository.SaveAllChangesAsync();
            if (result)
                return Ok(cartDto);

            return BadRequest("Failed to update shopping cart");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CartDto>> GetShoppingCart()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await userRepository.GetUserByUsernameAsync(username);

            var cart = await shoppingCartRepository.GetCartDtoByUserIdAsync(user.Id);

            if (cart == null)
                return Ok();

            return Ok(cart);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteShoppingCart()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await userRepository.GetUserByUsernameAsync(username);

            var cart = await shoppingCartRepository.GetCartByUserIdAsync(user.Id);
            if (cart == null)
                return NoContent();

            shoppingCartRepository.DeleteCart(cart);
            var result = await shoppingCartRepository.SaveAllChangesAsync();
            if (!result)
                return StatusCode(500);

            return NoContent();
        }
    }
}
