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
            var cart = mapper.Map<Cart>(cartDto);

            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await userRepository.GetUserByUsernameAsync(username);

            foreach(var ci in cart.CartItems)
            {
                // Maybe remove item if the quantity
                if (ci.Quantity <= 0 || ci.Quantity > 100)
                    ci.Quantity = 1;

               Product verifiedProduct = await productRepository.GetProductByIdAsync(ci.ProductId);
                if (verifiedProduct == null)
                    return BadRequest("Invalid Product");


                ci.Product = verifiedProduct;
            }

            var existingCart = await shoppingCartRepository.GetCartByUserIdAsync(user.Id);

            if (existingCart != null)
            {
                existingCart.CartItems = cart.CartItems;
                cart = existingCart;
            } 
            else {
                cart.User = user;
                shoppingCartRepository.AddCart(cart);
            }

            var updatedCart = mapper.Map<CartDto>(cart);

            var result = await shoppingCartRepository.SaveAllChangesAsync();
            if (result)
                return Ok(updatedCart);

            return BadRequest("Failed to update shopping cart");
        }

        [Authorize]
        [HttpPost("AddProduct")]
        public async Task<ActionResult> AddItemToShoppingCart(CartItemDto cartItemDto)
        {
            // We need to authincate here
            if (cartItemDto.Quantity <= 0)
                BadRequest("No cart data sent");

            var product = await productRepository.GetProductByIdAsync(cartItemDto.ProductId);
               // await context.Products.FindAsync(cartItemDto.ProductId);
            if(product == null)
                BadRequest("Product not found");

            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var user = await userRepository.GetUserByUsernameAsync(username);

            //check if we have an exist cart
            var cart = await shoppingCartRepository.GetCartByUserIdAsync(user.Id);

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

               shoppingCartRepository.AddCart(cart);

            } 
            else
            {
                if (cart.CartItems == null || !cart.CartItems.Any())
                    return BadRequest("Internal Server Error");

                CartItem cartItemExistingProduct;
                
                cartItemExistingProduct = cart.CartItems.SingleOrDefault(ci => ci.Product == product);

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

            var result = await shoppingCartRepository.SaveAllChangesAsync();
            if(result)
                return NoContent();

            return BadRequest("Failed to save shopping cart");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CartDto>> GetShoppingCart()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await userRepository.GetUserByUsernameAsync(username);

            var cart = await shoppingCartRepository.GetCartByUserIdAsync(user.Id);

            if (cart == null)
                return Ok();

            //Use auto mapper from cart to cartDto

            var cartDto = mapper.Map<CartDto>(cart);

            if (cartDto == null)
                return BadRequest("Failed to get cart");

            return Ok(cartDto);
        }
    }
}
