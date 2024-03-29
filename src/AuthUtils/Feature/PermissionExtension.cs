﻿using AuthUtils.PermissionParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthUtils.Feature;

public static class PermissionExtensions
{
    /// <summary>
    /// This returns true if the current user has the permission
    /// </summary>
    /// <param name="user"></param>
    /// <param name="permission"></param>
    /// <returns></returns>
    public static bool UserHasThisPermission(this ClaimsPrincipal user, Permissions permission)
    {
        var permissionClaim =
            user?.Claims.SingleOrDefault(x => x.Type == PermissionConstants.PackedPermissionClaimType);
        return permissionClaim?.Value.UnpackPermissionsFromString().ToArray().UserHasThisPermission(permission) == true;
    }

    public static string? GetUserIdFromClaims(this IEnumerable<Claim> claims)
    {
        return claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}

