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

        public async Task<IActionResult> Create(string name)
        {
            return ApiActionResult(await _roleService.CreateRole(name));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(string name, string newName)
        {
            return ApiActionResult(await _roleService.UpdateRole(name, newName));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string name)
        {
            return ApiActionResult(await _roleService.DeleteRole(name));
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

        [HttpPut("updatePermission")]
        public async Task<IActionResult> UpdatePermission(string roleName, string permission, string newPermission)
        {
            return ApiActionResult(await _roleService.UpdatePermission(roleName, permission, newPermission));
        }

        [HttpDelete("deletePermission")]
        public async Task<IActionResult> DeletePermission(string roleName, string permission)
        {
            return ApiActionResult(await _roleService.DeletePermission(roleName, permission));
        }

    }
}
