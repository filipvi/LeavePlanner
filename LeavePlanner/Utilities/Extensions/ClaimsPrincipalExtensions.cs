using System.Security.Claims;

namespace LeavePlanner.Utilities.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static readonly string ClaimTypeId = "Id";
        public static readonly string ClaimTypeName = "Name";
        public static readonly string ClaimTypeFullName = "FullName";
        public static readonly string ClaimTypeDomainName = "DomainName";

        /// <summary>
        ///     Populate employee claims
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Queryable user claims</returns>
        /// <exception cref="NotImplementedException"></exception>
        // public static IEnumerable<Claim> PrepareUserClaims(SifDjelatnik employee)
        // {
        //     var claims = new List<Claim>();
        //
        //     if (employee == null) return claims;
        //
        //     claims.Add(new Claim(ClaimTypeId, employee.Id.ToString()));
        //     claims.Add(new Claim(ClaimTypeName, employee.GetImeIPrezimeDjelatnika()));
        //     claims.Add(new Claim(ClaimTypeFullName, UserExtensions.GetImeIPrezimeDjelatnikaSaTitulom(employee)));
        //     claims.Add(new Claim(ClaimTypeDomainName, employee.DomenskoIme));
        //
        //     if (employee.VezaKorisnickaRolaDjelatniks != null &&
        //         employee.VezaKorisnickaRolaDjelatniks.Count > 0)
        //     {
        //         var korisnickaRolaNaziv = employee.VezaKorisnickaRolaDjelatniks
        //             .OrderByDescending(x => x.Id)
        //             .FirstOrDefault(x => x.Aktivna.HasValue && x.Aktivna.Value)
        //             ?.KorisnickaRola.Naziv;
        //
        //         if (korisnickaRolaNaziv != null)
        //         {
        //             if (!UserRoles.AllRoles.Contains(korisnickaRolaNaziv)) throw new Exception("Korisnička rola nije pronađena!");
        //
        //             claims.Add(new Claim(ClaimTypes.Role, korisnickaRolaNaziv));
        //         }
        //     }
        //
        //     return claims;
        // }

        /// <summary>
        ///     Check if user is authenticated
        /// </summary>
        /// <param name="user"></param>
        /// <returns>bool true or false</returns>
        public static bool IsAuthenticated(this ClaimsPrincipal user)
        {
            var isAuthenticated = false;

            if (user.Identity != null)
                isAuthenticated = user.Identity.IsAuthenticated;

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
        public static string GetLoggedInUserDomainName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypeDomainName);
        }

        /// <summary>
        ///     Returning logged user first and last name
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetLoggedInUserImePrezime(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypeName);
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