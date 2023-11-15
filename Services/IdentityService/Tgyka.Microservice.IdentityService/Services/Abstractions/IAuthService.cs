using System.Threading.Tasks;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.IdentityService.Models;

namespace Tgyka.Microservice.IdentityService.Services.Abstractions
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthModel>> Login(LoginModel model);
        Task<ApiResponse<AuthModel>> Register(RegisterModel model);
    }
}
