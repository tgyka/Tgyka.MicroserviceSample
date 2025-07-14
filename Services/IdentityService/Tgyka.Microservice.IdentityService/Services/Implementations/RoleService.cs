using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.IdentityService.Data.Entities;
using Tgyka.Microservice.IdentityService.Services.Abstractions;

namespace Tgyka.Microservice.IdentityService.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<ApiResponse<string>> CreateRole(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
                return ApiResponse<string>.Error(400, "Role already exists");

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
                return ApiResponse<string>.Success(201, "Role created");

            return ApiResponse<string>.Error(400, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<ApiResponse<string>> AssignRoleToUser(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ApiResponse<string>.Error(404, "User not found");

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
                return ApiResponse<string>.Success(200, "Role assigned");

            return ApiResponse<string>.Error(400, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<ApiResponse<string>> AddPermissionToRole(string roleName, string permission)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return ApiResponse<string>.Error(404, "Role not found");

            var claim = new Claim("Permission", permission);
            var existing = await _roleManager.GetClaimsAsync(role);
            if (existing.Any(c => c.Type == claim.Type && c.Value == claim.Value))
                return ApiResponse<string>.Error(400, "Permission already exists");

            var result = await _roleManager.AddClaimAsync(role, claim);
            if (result.Succeeded)
                return ApiResponse<string>.Success(200, "Permission added");

            return ApiResponse<string>.Error(400, result.Errors.Select(e => e.Description).ToArray());
        }
    }
}
