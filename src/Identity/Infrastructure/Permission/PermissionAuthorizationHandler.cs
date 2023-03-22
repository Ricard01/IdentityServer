using System.Linq;
using Identity.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Identity.Infrastructure.Permission
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
      
        private readonly ILogger<PermissionAuthorizationHandler> _logger;
        private string _identityServerUrl;


        public PermissionAuthorizationHandler(ILogger<PermissionAuthorizationHandler> logger, IConfiguration configuration)
        {
            _logger = logger;
            _identityServerUrl = configuration["Identity:Authority"];
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            // var permissionsStrings = string.Join(',', requirement.Permissions);
            // _logger.LogWarning("Permissions: "+permissionsStrings);
            //
            // var contextStrings = string.Join(',', context.User.Claims.Select(x => x.Value));
            // _logger.LogWarning("Claims: "+contextStrings);
            //
            // var claimTypes = string.Join(',', context.User.Claims.Select(x => x.Type));
            // _logger.LogWarning("Claim Types: "+claimTypes);
            //
            // var issuersStrings = string.Join(',', context.User.Claims.Select(x => x.Issuer));
            // _logger.LogWarning("Issuers: "+issuersStrings);
            
            var contextClaims = context.User.FindAll(c => c.Type == PermissionRequirement.ClaimType);
            if (requirement.PermissionOperator == PermissionOperator.And)
            {

                foreach (var permission in requirement.Permissions)
                {
                    // _logger.LogWarning("Permission requested: " + permission);
                    if (!context.User.HasClaim( c => c.Type == PermissionRequirement.ClaimType && c.Value == permission && c.Issuer == _identityServerUrl ))
                    {
                        _logger.LogWarning("Current user's does not satisfy the permission authorization requirement "+permission, requirement.Permissions);
                        context.Fail();
                        return Task.CompletedTask;
                    }
                }

                // identity has all required permissions
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            // OR
            foreach (var permission in requirement.Permissions)
            {
                // if has any permission then succeed 
                if (context.User.HasClaim(c => c.Type == PermissionRequirement.ClaimType && c.Value == permission && c.Issuer == _identityServerUrl))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                } 
                else
                { 
                    // _logger.LogWarning("Expected Claim type: "+PermissionRequirement.ClaimType);
                    // _logger.LogWarning("Expected Issuer: "+AppConfig.IdentityServerURL);
                    // _logger.LogWarning("Expected permission: "+permission);
                    // _logger.LogWarning("Current user's does not satisfy the permission authorization requirement", permission);

                }
            }

            // identity does not have any of the required permissions
            context.Fail();
            return Task.CompletedTask;
        }
    }
   

}
