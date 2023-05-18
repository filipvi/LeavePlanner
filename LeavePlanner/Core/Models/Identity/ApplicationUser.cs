using Microsoft.AspNetCore.Identity;

namespace LeavePlanner.Core.Models.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
