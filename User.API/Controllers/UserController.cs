﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using User.Data.DTO;
using User.Data.Models;
using User.Domain.Services.Interfaces;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] RegistrationPersonDTO person)
        {
            return Ok(await _userService.AddUserAsync(person));
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok( await _userService.GetByIdAsync(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            await _userService.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PersonDTO personForUpdate)
        {
            return Ok(await _userService.UpdateUserAsync(personForUpdate));
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAmount([FromQuery] int skip, [FromQuery] int take)
        {
            return Ok(await _userService.GetLimitedUsers(skip, take));
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCountOfUsers()
        {
            return Ok(await _userService.GetCountOfUsers());
        }

    }
}
