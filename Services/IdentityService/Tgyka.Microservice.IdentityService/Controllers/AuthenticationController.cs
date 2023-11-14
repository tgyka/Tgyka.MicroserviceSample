using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MssqlRestApi.Base.Controller;
using System.Threading.Tasks;
using Tgyka.Microservice.IdentityService.Models;
using Tgyka.Microservice.IdentityService.Services.Abstractions;

namespace Tgyka.Microservice.IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : TgykaMicroserviceControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authenticationService)
        {
            _authService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            return ApiActionResult(await _authService.Login(model));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            return ApiActionResult(await _authService.Register(model));
        }
    }
}
