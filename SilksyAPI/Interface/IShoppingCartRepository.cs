using SilksyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Interface
{
    public interface IShoppingCartRepository
    {
        Task<Cart> GetCartByIdAsync(int id);

        Task<Cart> GetCartByUserIdAsync(int userId);
        void AddCart(Cart cart);
        Task<bool> SaveAllChangesAsync();
    }
}
