using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using Duende.IdentityServer;
using IdentityModel;
using System.Security.Claims;
using System.Text.Json;

namespace IdentityServer6.Infrastructure.Persistence;

public static class ConfigIDS
{

    public static List<TestUser> Users
    {
        get
        {
            var address = new
            {
                street_address = "One Hacker Way",
                locality = "Heidelberg",
                postal_code = 69118,
                country = "Germany"
            };

            return new List<TestUser>
    {
      new TestUser
      {
        SubjectId = "818727",
        Username = "alice",
        Password = "alice",
        Claims =
        {
          new Claim(JwtClaimTypes.Name, "Alice Smith"),
          new Claim(JwtClaimTypes.GivenName, "Alice"),
          new Claim(JwtClaimTypes.FamilyName, "Smith"),
          new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
          new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
          new Claim(JwtClaimTypes.Role, "admin"),
          new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
          new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
            IdentityServerConstants.ClaimValueTypes.Json)
        }
      },
      new TestUser
      {
        SubjectId = "88421113",
        Username = "bob",
        Password = "bob",
        Claims =
        {
          new Claim(JwtClaimTypes.Name, "Bob Smith"),
          new Claim(JwtClaimTypes.GivenName, "Bob"),
          new Claim(JwtClaimTypes.FamilyName, "Smith"),
          new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
          new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
          new Claim(JwtClaimTypes.Role, "user"),
          new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
          new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
            IdentityServerConstants.ClaimValueTypes.Json)
        }
      }
    };
        }
    }

    public static IEnumerable<IdentityResource> IdentityResources =>
      new[]
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
      new[]
      {
    new ApiScope("weatherapi.read"),
    new ApiScope("weatherapi.write"),
      };

    public static IEnumerable<ApiResource> ApiResources =>
      new[]
    {
  new ApiResource("weatherapi")
  {
    Scopes = new List<string> {"weatherapi.read", "weatherapi.write"},
    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
    UserClaims = new List<string> {"role"}
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

      AllowedScopes = {"weatherapi.read", "weatherapi.write"}
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
      AllowedScopes = {"openid", "profile", "weatherapi.read"},
      RequirePkce = true,
      RequireConsent = true,
      AllowPlainTextPkce = false
    },
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
            "offline_access",
            "weatherapi.write"


        },

    },
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
      AllowedScopes = {"openid", "profile", "weatherapi.read"},
     // RequireConsent = Default false, 
      // AllowPlainTextPkce = Default false
    },
      };


}
