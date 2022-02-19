using SilksyAPI.Dto;
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
        Task<CartDto> GetCartDtoByUserIdAsync(int userId);
        void AddCart(Cart cart);

        void AddCartAsync(Cart cart);
        Task<bool> SaveAllChangesAsync();
        void DeleteCart(Cart cart);
    }
}
