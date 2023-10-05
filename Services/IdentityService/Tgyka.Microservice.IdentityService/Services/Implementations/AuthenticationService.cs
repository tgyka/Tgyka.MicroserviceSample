using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.IdentityService.Data.Entities;
using Tgyka.Microservice.IdentityService.Models;
using System.Linq;
using Tgyka.Microservice.IdentityService.Services.Abstractions;

namespace Tgyka.Microservice.IdentityService.Services.Implementations
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<string> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return "Error";
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, lockoutOnFailure: false);

            return GenerateJwtToken(user);
        }

        public async Task<string> Register(RegisterModel model)
        {
            var user = new ApplicationUser { UserName = model.Username, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return "Registration completed successfully";
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description);
                return string.Join(", ", errors);
            }
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("your-secret-key"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                var token = new JwtSecurityToken(
                    issuer: "your-issuer",
                    audience: "your-audience",
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30), 
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
