using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SilksyAPI.Data;
using SilksyAPI.Dto;
using SilksyAPI.Entities;
using SilksyAPI.Interface;
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
        private readonly UserManager<StoreUser> userManager;
        private readonly SignInManager<StoreUser> signInManager;
        private readonly IMapper mapper;
        private readonly ILogger<AccountController> logger;
        private readonly ITokenService tokenService;

        public AccountController(UserManager<StoreUser> userManager, SignInManager<StoreUser> signInManager, IMapper mapper, 
            ILogger<AccountController> logger, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.logger = logger;
            this.tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            
            if (await userManager.FindByNameAsync(registerDto.Username) != null)
                return BadRequest("Username is taken");

            // Map Data
            var user = mapper.Map<StoreUser>(registerDto);

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return new UserDto
            {
                Username = user.UserName,
                Token = tokenService.CreateToken(user),
                Email = user.Email
            };
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.Username); // context.Users.SingleOrDefaultAsync(storeUser => storeUser.UserName == loginDto.Username);
            if (user == null)
                return BadRequest("Username or password is incorrect");

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
                BadRequest("Username or password is incorrect");

            return new UserDto
            {
                Username = user.UserName,
                Token = tokenService.CreateToken(user),
                Email = user.Email
            };
        }
    }
}
