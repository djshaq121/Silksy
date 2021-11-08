using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SilksyAPI.Data;
using SilksyAPI.Dto;
using SilksyAPI.Entities;
using SilksyAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.SilksyRepository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly SilksyContext context;
        private readonly IMapper mapper;

        public ShoppingCartRepository(SilksyContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            return await context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .SingleOrDefaultAsync(cart => cart.UserId == id);
        }

        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            return await context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .SingleOrDefaultAsync(cart => cart.UserId == userId);
        }

        public async Task<CartDto> GetCartDtoByUserIdAsync(int userId)
        {
            return await context.Carts
                .Where(x => x.UserId == userId)
                .ProjectTo<CartDto>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            //.Include(c => c.CartItems)
            //        .ThenInclude(ci => ci.Product)
            //            .ThenInclude(p => p.Brand)
            //     .Include(c => c.CartItems)
            //        .ThenInclude(ci => ci.Product)
            //            .ThenInclude(ci => ci.ProductCategories)
        }

        public void AddCart(Cart cart)
        {
            context.Carts.Add(cart);
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void DeleteCart(Cart cart)
        {
            context.Carts.Remove(cart);
        }
    }
}
