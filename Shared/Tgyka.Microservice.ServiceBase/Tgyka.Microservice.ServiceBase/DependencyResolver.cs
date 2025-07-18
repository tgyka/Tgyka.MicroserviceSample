﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using Tgyka.Microservice.Base.Model.Token;

namespace Tgyka.Microservice.Base
{
    public static class DependencyResolver
    {
        public static void AddAuthenticationAndBindTokenUser(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<TokenUser>();

            services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidIssuer = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var token = context.SecurityToken as JwtSecurityToken;
                        if (token != null)
                        {
                            var tokenUser = context.HttpContext.RequestServices.GetRequiredService<TokenUser>();

                            tokenUser.Email = token.Claims.First(x => x.Type == "Email").Value;
                            tokenUser.Id = token.Claims.First(x => x.Type == "UserId").Value;
                            tokenUser.Username = token.Claims.First(x => x.Type == "Username").Value;
                            tokenUser.AccessToken = token.RawData;
                            tokenUser.Roles = token.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
                            tokenUser.Permissions = token.Claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
