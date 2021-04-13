using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using User.Data.DTO;
using User.Data.Models;
using User.Domain.Services.Interfaces;

namespace User.API.Controllers
{
    [Route("api/users")]
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
        [AllowAnonymous]
        public async Task<IActionResult> SignUpUser([FromBody] RegistrationPersonDTO person)
        {
            return Ok(await _userService.SignUpUserAsync(person));
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddUser([FromBody] PersonDTO person)
        {
            return Ok(await _userService.AddUserAsync(person));
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

        [HttpGet]
        public async Task<IActionResult> GetLimitedUsers([FromQuery] QueryParamsDTO param)
        {
            if (String.IsNullOrEmpty(param.SortBy))
            {
                return Ok(await _userService.GetLimitedUsers(param.Skip, param.Take));
            }

            if (param.Order == "asc")
            {
                return Ok(await _userService.GetSortedUsers(param.SortBy, param.Skip, param.Take, param.Order));
            }

            return Ok(await _userService.GetSortedUsers(param.SortBy, param.Skip, param.Take, param.Order));
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCountOfUsers()
        {
            return Ok(await _userService.GetCountOfUsers());
        }
    }
}
