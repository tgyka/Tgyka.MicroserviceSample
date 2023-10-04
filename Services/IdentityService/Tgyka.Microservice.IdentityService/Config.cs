using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Tgyka.Microservice.IdentityService
{
    public class Config
    {
        // Identity Resources (Kimlik Kaynakları)
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            };

        // API Resources (API Kaynakları)
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
            new ApiResource("api1", "My API")
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                        new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        // Clients (İstemciler)
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
            new Client
            {
                ClientId = "client_id",
                ClientSecrets = { new Secret("client_secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // Örnek olarak Resource Owner Password Credentials grant tipi
                AllowedScopes = { "api1" }
            }
            };
    }
}
