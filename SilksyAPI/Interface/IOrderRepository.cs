using SilksyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Interface
{
    public interface IOrderRepository
    {
        public Task<Order> GetUserOrderAsync(int orderId, StoreUser user);

        public Task<IReadOnlyCollection<Order>> GetUserOrdersAsync(StoreUser user);
    }
}
