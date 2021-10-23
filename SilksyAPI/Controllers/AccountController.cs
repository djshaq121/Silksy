using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SilksyAPI.Data;
using SilksyAPI.Dto;
using SilksyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SilksyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SilksyContext context;
        private readonly IMapper mapper;
        private readonly ILogger<AccountController> logger;

        public AccountController(SilksyContext context, IMapper mapper, ILogger<AccountController> logger)
        {
            this.context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userNameFound = await context.Users.SingleOrDefaultAsync(storeUser => storeUser.UserName == registerDto.Username);
            if (userNameFound != null)
                return BadRequest("A User with this username already exist");

            var emailFound = await context.Users.SingleOrDefaultAsync(storeUser => storeUser.Email == registerDto.Email);
            if (emailFound != null)
                return BadRequest("A User with this email already exist");

            // Map Data
            var user = mapper.Map<StoreUser>(registerDto);

            //Hash password 
            using var hmac = new HMACSHA512();

            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            user.PasswordSalt = hmac.Key;

            //Add to table tracking but not to db thats why we dont need to await
             context.Users.Add(user);

            // Save Databse
            try
            {
             var result = await context.SaveChangesAsync();

            } catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            return new UserDto
            {
              Username = user.UserName,
              Token = "Test Token"
            };
        }

        [HttpPost("Login")]
        public ActionResult<UserDto> Login(LoginDto login)
        {
            string user = login.Username;

            return new UserDto
            {
                Username = user
            };
        }

    }
}
