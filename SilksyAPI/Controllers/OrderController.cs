using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SilksyAPI.Data;
using SilksyAPI.Dto;
using SilksyAPI.Entities;
using SilksyAPI.Helpers;
using SilksyAPI.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SilksyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly SilksyContext context;
        private readonly IPaymentService paymentService;
        private readonly IUserRepository userRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IMapper mapper;

        public OrderController(SilksyContext context, IPaymentService paymentService, IUserRepository userRepository, 
            IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            this.context = context;
            this.paymentService = paymentService;
            this.userRepository = userRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.mapper = mapper;
        }

        [HttpPost("CreateOrder")]
        [Authorize]
        public async Task<ActionResult> CreateOrder(OrderParams orderParams)
        {
            AddressDto addressDto = orderParams.ShippingAddress;
            var shippingAddress = mapper.Map<Entities.Address>(addressDto);

            if (shippingAddress == null)
                return BadRequest("Invalid Order Parameters");

            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await userRepository.GetUserByUsernameAsync(username);

            //if(user.Addresses?.Any() != true) {
            //    var userAddress = new UserAddress { User = user, Address = address };

            //    var userAddresses = new List<UserAddress>();
            //    userAddresses.Add(userAddress);

            //    user.Addresses = userAddresses;
            //    address.UserAddresses = userAddresses;

            //    context.UserAddresses.Add(userAddress);
            //}

            var cart = await shoppingCartRepository.GetCartByUserIdAsync(user.Id);

            if (cart == null)
                return BadRequest("Failed to get user cart");

            if (!cart.CartItems.Any())
                return BadRequest("No Items in cart to buy");

           var orderItems = mapper.Map<ICollection<OrderItem>>(cart.CartItems);

            if (!orderItems.Any())
                return BadRequest("Server Error");

           var order = new Order(user, orderItems, shippingAddress, orderParams.PaymentIntentId);

           context.Orders.Add(order);

           var result = await context.SaveChangesAsync();
           if (result > 0)
                return Ok(new { OrderId = order.Id });

            return BadRequest("Server Error");
        }
    }
}
