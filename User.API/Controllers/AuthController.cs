using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        private readonly IOptions<AuthOptions> _authOptions;
        public AuthController(IUserService userService, IOptions<AuthOptions> authOptions, IAuthService authService)
        {
            _userService = userService;
            _authOptions = authOptions;
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            
            
            
            var user = await _authService.AuthenticateUser(login.Email, login.Password);

            if (user != null)
            {
                var token = _authService.GenerateJWT(user);
                user.Token = token;
                return Ok(user);
            }
            return Unauthorized();
        }
    }
}
