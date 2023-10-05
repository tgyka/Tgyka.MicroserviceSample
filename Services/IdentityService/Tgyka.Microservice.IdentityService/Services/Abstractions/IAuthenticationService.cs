using System.Threading.Tasks;
using Tgyka.Microservice.IdentityService.Models;

namespace Tgyka.Microservice.IdentityService.Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<string> Login(LoginModel model);
        Task<string> Register(RegisterModel model);
    }
}
