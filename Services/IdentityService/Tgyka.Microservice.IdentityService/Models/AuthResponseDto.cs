﻿namespace Tgyka.Microservice.IdentityService.Models
{
    public class AuthResponseDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
