using LeavePlanner.Core.Models.Identity;
using System.Security.Claims;

namespace LeavePlanner.Utilities.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static readonly string ClaimTypeId = "Id";
        public static readonly string ClaimTypeRole = "Role";
        public static readonly string ClaimTypeFullName = "FullName";

        public static IEnumerable<Claim> PrepareUserClaims(ApplicationUser user)
        {
            var claims = new List<Claim>();

            if (user == null)
            {
                return claims;
            }

            claims.Add(new Claim(ClaimTypeId, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypeFullName, user.FirstName + " " + user.LastName));

            var roleName = user.UserRoles.FirstOrDefault()?.Role.Name;
            if (roleName != null)
            {
                claims.Add(new Claim(ClaimTypeRole, roleName));
            }

            return claims;
        }

        /// <summary>
        ///     Check if user is authenticated
        /// </summary>
        /// <param name="user"></param>
        /// <returns>bool true or false</returns>
        public static bool IsAuthenticated(this ClaimsPrincipal user)
        {
            var isAuthenticated = false;

            if (user.Identity != null) isAuthenticated = user.Identity.IsAuthenticated;

            return isAuthenticated;
        }

        /// <summary>
        ///     Returning logged user id
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public static string GetLoggedInUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypeId);
        }

        public static T GetLoggedInUserId<T>(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var loggedInUserId = principal.FindFirstValue(ClaimTypeId);

            return (T)Convert.ChangeType(loggedInUserId, typeof(T));
        }

        /// <summary>
        ///     Returning logged username
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetLoggedInUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Email);
        }


        /// <summary>
        ///     Returning logged user full name
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetLoggedInUserFullName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypeFullName);
        }

        /// <summary>
        ///     Returning logged user role
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetLoggedInUserRole(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Role);
        }

    }
}