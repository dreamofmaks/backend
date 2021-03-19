using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Data.DTO;
using User.Data.Models;
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
        public async Task<IActionResult> AddUser([FromBody] PersonDTO person)
        {
            return Ok(await userService.AddUserAsync(person));
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok( await userService.GetByIdAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await userService.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PersonDTO personForUpdate)
        {
            return Ok(await userService.UpdateUserAsync(personForUpdate));
        }
    }
}
