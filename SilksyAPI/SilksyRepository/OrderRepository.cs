using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SilksyAPI.Data;
using SilksyAPI.Dto;
using SilksyAPI.Entities;
using SilksyAPI.Helpers;
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
        private readonly IMapper mapper;

        public OrderRepository(SilksyContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Order> GetUserOrderAsync(int orderId, StoreUser user)
        {
            return await context.Orders
                 .Where(o => o.UserId == user.Id)
                 .Include(o => o.OrderItems)
                 .ThenInclude(oi => oi.Product)
                 .Include(o => o.ShippingAddress)
                 .SingleOrDefaultAsync(order => order.Id == orderId);
        }

        public async Task<PagedList<Order>> GetUserOrdersAsync(StoreUser user, OrderParams orderParams)
        {
            var query = context.Orders
                   .AsQueryable()
                   .Where(o => o.UserId == user.Id)
                   .OrderByDescending(o => o.OrderDate)
                   .Include(o => o.OrderItems)
                   .ThenInclude(oi => oi.Product)
                   .Include(o => o.ShippingAddress);

          return await PagedList<Order>.CreateAsync(query, orderParams.PageNumber, orderParams.PageSize);
                 
        }
    }
}
//await context.Orders
//                 .OrderByDescending(o => o.OrderDate)
//                 .Include(o => o.OrderItems)
//                 .ThenInclude(oi => oi.Product)
//                 .Include(o => o.ShippingAddress)
//                 .ToListAsync();

//return await context.Orders
//                .ProjectTo<OrderDto>(mapper.ConfigurationProvider)
//                .OrderByDescending(o => o.OrderDate)
//                .ToListAsync();