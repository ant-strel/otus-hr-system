using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("SwaggerAPI_WF", "Swagger API DEMO WF"),
                new ApiScope("SwaggerAPI_Portal", "Swagger API DEMO Portal")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                    {
                        Name = "roles",
                        DisplayName = "Roles",
                        Description = "Allow the service access to your user roles.",
                        UserClaims = new[] { JwtClaimTypes.Role, ClaimTypes.Role },
                        ShowInDiscoveryDocument = true,
                        Required = true,
                        Emphasize = true
                    }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("SwaggerAPI_WF", "Swagger API DEMO WF", new [] { JwtClaimTypes.Name })
                {
                    Scopes=
                    {
                        "SwaggerAPI_WF",
                    },
                    UserClaims = {"role"},                    
                },
                new ApiResource("SwaggerAPI_Portal", "Swagger API DEMO Portal", new [] { JwtClaimTypes.Name })
                {
                    Scopes=
                    {
                        "SwaggerAPI_Portal",
                    },
                    UserClaims = {"role"},
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client_id_swagger_wf",
                    ClientName = "Swagger client wf",
                    ClientSecrets = { new Secret("client_secret_swagger_wf".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedCorsOrigins = {"http://localhost:7000"},
                    AllowedScopes =
                    {
                        "SwaggerAPI_WF", 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "role"
                    },
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = 3600,
                    RequireClientSecret = false,
                    RedirectUris =
                    {
                        "http://localhost:7000/swagger/index.html"
                    },
                     AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowAccessTokensViaBrowser = true,
                    RequirePkce = true,
                },

                 new Client
                {
                    ClientId = "client_id_swagger_portal",
                    ClientName = "Swagger client portal",
                    ClientSecrets = { new Secret("client_secret_swagger_portal".ToSha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedCorsOrigins = {"http://localhost:85"},
                    AllowedScopes =
                    {
                        "SwaggerAPI_Portal",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "role"
                    },
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = 3600,
                    RequireClientSecret = false,
                    RedirectUris =
                    {
                        "http://localhost:85/swagger/index.html"
                    },
                     AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowAccessTokensViaBrowser = true,
                    RequirePkce = true,
                }
            };
    }
}

