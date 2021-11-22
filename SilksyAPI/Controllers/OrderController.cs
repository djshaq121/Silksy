using Microsoft.AspNetCore.Mvc;
using SilksyAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly SilksyContext context;

        public OrderController(SilksyContext context)
        {
            this.context = context;
        }

        //public async Task<ActionResult> CreateOrder()
        //{

        //}
    }
}
