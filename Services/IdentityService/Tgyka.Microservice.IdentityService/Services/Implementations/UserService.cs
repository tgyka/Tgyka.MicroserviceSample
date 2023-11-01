using Microsoft.AspNetCore.Identity;
using Tgyka.Microservice.IdentityService.Data.Entities;
using Tgyka.Microservice.IdentityService.Services.Abstractions;

namespace Tgyka.Microservice.IdentityService.Services.Implementations
{
    public class UserService: IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
    }
}
