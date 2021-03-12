using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Data.DTO;
using User.Data.Model;
using User.Domain.Services.Interfaces;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        public IActionResult AddUser([FromBody] PersonDTO person)
        {
            return Ok(userService.AddUser(person));
        }
    }
}
