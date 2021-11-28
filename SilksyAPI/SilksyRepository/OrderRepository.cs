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
    public class OrderRepository : IOrderRepository
    {
        private readonly SilksyContext context;

        public OrderRepository(SilksyContext context)
        {
            this.context = context;
        }

        public async Task<Order> GetUserOrderAsync(int orderId, StoreUser user)
        {
            return await context.Orders
                 .Include(o => o.OrderItems)
                 .ThenInclude(oi => oi.Product)
                 .Include(o => o.ShippingAddress)
                 .SingleOrDefaultAsync(order => order.Id == orderId);
        }

        public Task<IReadOnlyCollection<Order>> GetUserOrdersAsync(StoreUser user)
        {
            throw new NotImplementedException();
        }
    }
}
