using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using User.Data.DTO;
using User.Data.Models;
using User.Domain.Services.Implementation;
using User.Domain.Services.Interfaces;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public AuthController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var authUser = await _authService.AuthenticateUser(login.Email, login.Password);
            if (authUser != null)
            {
                return Ok(authUser);
            }

            return BadRequest("Wrong email or password!");
        }
    }
}
