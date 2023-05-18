#region

using Microsoft.AspNetCore.Authorization;

#endregion

namespace LeavePlanner.Utilities.Security;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }
}