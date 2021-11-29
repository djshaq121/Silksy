using SilksyAPI.Dto;
using SilksyAPI.Entities;
using SilksyAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Interface
{
    public interface IOrderRepository
    {
        public Task<Order> GetUserOrderAsync(int orderId, StoreUser user);

        public Task<PagedList<Order>> GetUserOrdersAsync(StoreUser user, OrderParams orderParams);
    }
}
