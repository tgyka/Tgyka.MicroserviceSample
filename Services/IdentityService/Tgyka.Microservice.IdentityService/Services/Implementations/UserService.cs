using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.IdentityService.Data.Entities;
using Tgyka.Microservice.IdentityService.Services.Abstractions;
using Tgyka.Microservice.MssqlBase.Model.RepositoryDtos;

namespace Tgyka.Microservice.IdentityService.Services.Implementations
{
    public class UserService: IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApiResponse<ApplicationUser>> GetUserByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            return ApiResponse<ApplicationUser>.Success(200, user);
        }

        public async Task<ApiResponse<PaginationModel<ApplicationUser>>> GetAllUsers(int page,int size)
        {
            var users = await _userManager.Users.Skip((page - 1) * size).Take(size).ToListAsync();
            var count = _userManager.Users.Count();

            return ApiResponse<PaginationModel<ApplicationUser>>.Success(200, new PaginationModel<ApplicationUser>(users, count,page,size));
        }

        public async Task<ApiResponse<ApplicationUser>> UpdateUser(ApplicationUser user)
        {
            await _userManager.UpdateAsync(user);
            return ApiResponse<ApplicationUser>.Success(200, user);
        }

        public async Task<ApiResponse<string>> DeleteUser(ApplicationUser user)
        {
            await _userManager.DeleteAsync(user);
            return ApiResponse<string>.Success(200, "User successfully deleted");
        }
    }
}
