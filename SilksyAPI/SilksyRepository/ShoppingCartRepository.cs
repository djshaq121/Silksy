using Microsoft.EntityFrameworkCore;
using SilksyAPI.Data;
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

        public ShoppingCartRepository(SilksyContext context)
        {
            this.context = context;
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

        public void AddCart(Cart cart)
        {
            context.Carts.Add(cart);
        }

        public async Task<bool> SaveAllChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
