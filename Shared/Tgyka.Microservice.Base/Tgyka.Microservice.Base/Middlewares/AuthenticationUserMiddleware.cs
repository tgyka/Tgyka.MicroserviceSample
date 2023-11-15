using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tgyka.Microservice.Base.Model.Token;

namespace Tgyka.Microservice.Base.Middlewares
{
    public class AuthenticationUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;


        public AuthenticationUserMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IServiceCollection services)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                BindTokenToUser(context, token, services);

            await _next(context);
        }

        private void BindTokenToUser(HttpContext context, string token, IServiceCollection services)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("YourSecretKey");

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]))
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                services.AddScoped(sp =>
                {
                    return new TokenUser
                    {
                        Email = jwtToken.Claims.First(x => x.Type == "Email").Value,
                        Id = jwtToken.Claims.First(x => x.Type == "UserId").Value,
                        Username = jwtToken.Claims.First(x => x.Type == "Username").Value,
                        Jwt = token
                    };
                });
            }
            catch (Exception exception)
            {
                context.Response.StatusCode = 401;

                Console.WriteLine($"Error: {exception}");
            }

        }
    }
}
