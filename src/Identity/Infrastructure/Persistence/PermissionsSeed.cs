using Identity.Domain.Permissions;
using Identity.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Persistence
{
    //TODO Ask: Is there a problem with passing context multiple times -inheritance-
    public class PermissionsSeed
    {
        public static async Task InitializeAsync(ApplicationDbContext context)
        {
            await AddTestData(context);
        }

        public static async Task AddTestData(ApplicationDbContext context)
        {
            if (context.ApiPermissions.Any())
            {
                // Already has data
                return;
            }

            await GeneratePermissions(context, typeof(Permissions.Catalogs));
            await GeneratePermissions(context, typeof(Permissions.Distribution));
            await GeneratePermissions(context, typeof(Permissions.Inventory));
            await GeneratePermissions(context, typeof(Permissions.Order));
            await GeneratePermissions(context, typeof(Permissions.User));

        }

        public static async Task GeneratePermissions(ApplicationDbContext context, Type type)
        {
            var apiPermissions = new List<ApplicationPermission>();

            var permissions = Permissions.GeneratePermissionsForModule(type);

            foreach (var perm in permissions)
            {
                apiPermissions.Add(new ApplicationPermission
                {
                    Name = perm?.Split('.').Last(),
                    Module = type.Name,
                    ClaimType = AppClaimTypes.Permissions,
                    ClaimValue = perm
                });
            }
            context.AddRange(apiPermissions);

            await context.SaveChangesAsync();
        }

    }

}
