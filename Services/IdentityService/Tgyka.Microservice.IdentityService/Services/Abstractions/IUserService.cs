using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.IdentityService.Data.Entities;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;

namespace Tgyka.Microservice.IdentityService.Services.Abstractions
{
    public interface IUserService
    {
        Task<ApiResponse<string>> DeleteUser(ApplicationUser user);
        Task<ApiResponse<ApplicationUser>> GetUserByUsername(string username);
        Task<ApiResponse<PaginationList<ApplicationUser>>> ListUsers(int page, int size);
        Task<ApiResponse<ApplicationUser>> UpdateUser(ApplicationUser user);
    }
}
