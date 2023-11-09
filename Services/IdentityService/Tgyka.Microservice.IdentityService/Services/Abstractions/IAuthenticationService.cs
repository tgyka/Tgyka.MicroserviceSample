using System.Threading.Tasks;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Tgyka.Microservice.IdentityService.Models;

namespace Tgyka.Microservice.IdentityService.Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<AuthResponseDto>> Login(LoginModel model);
        Task<ApiResponse<AuthResponseDto>> Register(RegisterModel model);
    }
}
