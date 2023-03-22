using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace IdentityServer6.Infrastructure.Persistence;

public static class ConfigIDS
{
 
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string> {"role"}
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
          {
            new ApiScope("weatherapi.read"),
            new ApiScope("weatherapi.write"),
            new ApiScope("identity.api"),
          };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
            {
              new ApiResource("weatherapi")
              {
                Scopes = new List<string> {"weatherapi.read", "weatherapi.write"},
                ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                UserClaims = new List<string> {"role"}
              },
              new ApiResource("identityApi")
              {
                   Scopes = new List<string> {"identity.api"},
                  ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha256())}
              }
            };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            // m2m client credentials flow client for APIS
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                AllowedScopes = {"weatherapi.read", "weatherapi.write", "identity.api" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = {"https://localhost:5444/signin-oidc"},
                FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                PostLogoutRedirectUris = {"https://localhost:5444/signout-callback-oidc"},

                AllowOfflineAccess = true,
                AllowedScopes = {"openid", "profile", "weatherapi.read","identity.api"},
                RequirePkce = true,
                RequireConsent = true,
                AllowPlainTextPkce = false
            },
    

            // Angular VS
            new Client
            {
                ClientId = "angularVS",
                ClientName = "angular From Visual Studio",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                AllowOfflineAccess = true,
                //AllowAccessTokensViaBrowser=true,
                RedirectUris = {"https://localhost:5002/callback"},
                PostLogoutRedirectUris = { "https://localhost:5002" },
                AllowedCorsOrigins={ "https://localhost:5002"}, // que pasa si no lo pongo 
                // RequireConsent = false, // default false
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "identity.api"
                },

            },

            // angular CMD
             new Client
            {
                ClientId = "angularCmd",
                ClientName = "Angular Native Client",
                AllowedGrantTypes = GrantTypes.Code,
                AllowAccessTokensViaBrowser=true,
                RequirePkce = true,
                RequireClientSecret = false,
                AllowOfflineAccess = true,
                //AllowAccessTokensViaBrowser=true,
                RedirectUris = {"https://localhost:5003/callback"},
                PostLogoutRedirectUris = {"https://localhost:5003/signout-callback-oidc"},
                //FrontChannelLogoutUri = "https://localhost:5003/signout-oidc",
                AllowedCorsOrigins = {"https://localhost:5003"},
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "weatherapi.read"
                },
                // RequireConsent = Default false, 
                // AllowPlainTextPkce = Default false
            },
        };


}
