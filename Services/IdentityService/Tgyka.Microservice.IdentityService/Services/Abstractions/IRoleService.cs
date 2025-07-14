using Tgyka.Microservice.Base.Model.ApiResponse;

namespace Tgyka.Microservice.IdentityService.Services.Abstractions
{
    public interface IRoleService
    {
        Task<ApiResponse<string>> CreateRole(string roleName);
        Task<ApiResponse<string>> UpdateRole(string roleName, string newRoleName);
        Task<ApiResponse<string>> DeleteRole(string roleName);

        Task<ApiResponse<string>> AssignRoleToUser(string userId, string roleName);

        Task<ApiResponse<string>> AddPermissionToRole(string roleName, string permission);
        Task<ApiResponse<string>> UpdatePermission(string roleName, string oldPermission, string newPermission);
        Task<ApiResponse<string>> DeletePermission(string roleName, string permission);
        Task<ApiResponse<string>> AssignRoleToUser(string userId, string roleName);
        Task<ApiResponse<string>> AddPermissionToRole(string roleName, string permission);
    }
}
