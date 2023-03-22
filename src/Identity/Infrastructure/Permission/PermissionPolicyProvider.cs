using Identity.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using static Identity.Infrastructure.Permission.PermissionAuthorizeAttribute;

namespace Identity.Infrastructure.Permission
{
  
        public class PermissionPolicyProvider : DefaultAuthorizationPolicyProvider
        {
            public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
                : base(options) { }

            
            public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
            {
                if (!policyName.StartsWith(PolicyPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    // it's not one of our dynamic policies, so we fallback to the base behavior
                    // this will load policies added in Startup.cs (AddPolicy..)
                    return await base.GetPolicyAsync(policyName);
                }

                PermissionOperator @operator = GetOperatorFromPolicy(policyName);
                string[] permissions = GetPermissionsFromPolicy(policyName);

                // extract the info from the policy name and create our requirement
                var requirement = new PermissionRequirement(@operator, permissions);

                // create and return the policy for our requirement
                return new AuthorizationPolicyBuilder().AddRequirements(requirement).Build();
            }
        }
       

        // For search engines: IAuthorizationPolicyProvider AllowAnonymous not working when using this in Razor Pages.
        // Since you cannot tag a handler method with AllowAnonymous you can’t use the Authorize attribute on the class and the AllowAnonymous attribute on the method.

        // Changing this:
        //      public Task GetFallbackPolicyAsync() => FallbackPolicyProvider.GetDefaultPolicyAsync();
        // to this:
        //      public Task GetFallbackPolicyAsync() => FallbackPolicyProvider.GetFallbackPolicyAsync();

        // Fixes the issue and allows classes to be tagged with[AllowAnonymous]

           
}
