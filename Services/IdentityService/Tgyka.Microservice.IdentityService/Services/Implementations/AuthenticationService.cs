using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.IdentityService.Data.Entities;
using Tgyka.Microservice.IdentityService.Models;
using System.Linq;
using Tgyka.Microservice.IdentityService.Services.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Tgyka.Microservice.IdentityService.Settings;
using Tgyka.Microservice.Base.Model.ApiResponse;

namespace Tgyka.Microservice.IdentityService.Services.Implementations
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<ApiResponseDto<AuthResponseDto>> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return ApiResponseDto<AuthResponseDto>.Error(400,"User is not found");
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, lockoutOnFailure: false);

            return ApiResponseDto<AuthResponseDto>.Success(200,GenerateJwtToken(user));
        }

        public async Task<ApiResponseDto<AuthResponseDto>> Register(RegisterModel model)
        {
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return ApiResponseDto<AuthResponseDto>.Success(200, GenerateJwtToken(user));
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description);
                return ApiResponseDto<AuthResponseDto>.Error(400,errors.ToArray());
            }
        }

        private AuthResponseDto GenerateJwtToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddMinutes(30);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                var token = new JwtSecurityToken(
                    issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: expireDate, 
                    signingCredentials: credentials);

                return new AuthResponseDto 
                { 
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpireDate = expireDate,
                    Id = user.Id,
                    Username = user.UserName,
                };
        }
    }
}
