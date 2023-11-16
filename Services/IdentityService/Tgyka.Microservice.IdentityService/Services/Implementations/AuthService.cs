using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using Tgyka.Microservice.IdentityService.Data.Entities;
using Tgyka.Microservice.IdentityService.Models;
using Tgyka.Microservice.IdentityService.Services.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Tgyka.Microservice.IdentityService.Settings;
using Tgyka.Microservice.Base.Model.ApiResponse;
using Microsoft.Extensions.Options;

namespace Tgyka.Microservice.IdentityService.Services.Implementations
{
    public class AuthService: IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<ApiResponse<AuthModel>> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return ApiResponse<AuthModel>.Error(400,"User is not found");
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, lockoutOnFailure: false);

            return ApiResponse<AuthModel>.Success(200,GenerateJwtToken(user));
        }

        public async Task<ApiResponse<AuthModel>> Register(RegisterModel model)
        {
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return ApiResponse<AuthModel>.Success(200, GenerateJwtToken(user));
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description);
                return ApiResponse<AuthModel>.Error(400,errors.ToArray());
            }
        }

        private AuthModel GenerateJwtToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinute);
            var claims = new List<Claim>
            {
                new("UserId", user.Id),
                new("Email", user.Email),
                new("Username", user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expireDate, 
                signingCredentials: credentials);

            return new AuthModel 
            { 
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireDate = expireDate,
                Id = user.Id,
                Username = user.UserName,
            };
        }
    }
}
