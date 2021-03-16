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

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(userService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            return Ok(userService.GetById(id));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            userService.DeleteById(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PersonDTO personForUpdate)
        {
            return Ok(userService.UpdateUser(id, personForUpdate));
        }
    }
}
