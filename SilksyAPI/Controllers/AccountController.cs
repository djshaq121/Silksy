using Microsoft.AspNetCore.Mvc;
using SilksyAPI.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public AccountController()
        {

        }

        [HttpPost("Register")]
        public ActionResult<UserDto> Register(RegisterDto registerDto)
        {
            string user = registerDto.Username;

            return new UserDto
            {
                Username = user
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
