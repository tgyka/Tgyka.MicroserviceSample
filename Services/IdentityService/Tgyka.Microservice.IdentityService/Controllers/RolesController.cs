using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MssqlRestApi.Base.Controller;
using Tgyka.Microservice.IdentityService.Services.Abstractions;

namespace Tgyka.Microservice.IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : TgykaMicroserviceControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(string name)
        {
            return ApiActionResult(await _roleService.CreateRole(name));
        }

        [HttpPost("assign")]
        public async Task<IActionResult> Assign(string userId, string roleName)
        {
            return ApiActionResult(await _roleService.AssignRoleToUser(userId, roleName));
        }

        [HttpPost("addPermission")]
        public async Task<IActionResult> AddPermission(string roleName, string permission)
        {
            return ApiActionResult(await _roleService.AddPermissionToRole(roleName, permission));
        }
    }
}
