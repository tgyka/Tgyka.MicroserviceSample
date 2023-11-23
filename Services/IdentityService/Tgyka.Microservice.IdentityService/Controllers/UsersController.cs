using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MssqlRestApi.Base.Controller;
using Tgyka.Microservice.IdentityService.Data.Entities;
using Tgyka.Microservice.IdentityService.Models;
using Tgyka.Microservice.IdentityService.Services.Abstractions;

namespace Tgyka.Microservice.IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : TgykaMicroserviceControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getUserByUsername")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            return ApiActionResult(await _userService.GetUserByUsername(username));
        }

        [HttpGet("list")]
        public async Task<IActionResult> ListUsers(int page,int size)
        {
            return ApiActionResult(await _userService.GetAllUsers(page,size));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(ApplicationUser user)
        {
            return ApiActionResult(await _userService.UpdateUser(user));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(ApplicationUser user)
        {
            return ApiActionResult(await _userService.DeleteUser(user));
        }
    }
}
