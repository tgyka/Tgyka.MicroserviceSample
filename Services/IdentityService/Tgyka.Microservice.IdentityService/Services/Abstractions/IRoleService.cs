using Tgyka.Microservice.Base.Model.ApiResponse;

namespace Tgyka.Microservice.IdentityService.Services.Abstractions
{
    public interface IRoleService
    {
        Task<ApiResponse<string>> CreateRole(string roleName);
        Task<ApiResponse<string>> AssignRoleToUser(string userId, string roleName);
        Task<ApiResponse<string>> AddPermissionToRole(string roleName, string permission);
    }
}
