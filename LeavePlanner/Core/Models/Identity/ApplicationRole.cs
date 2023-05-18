using Microsoft.AspNetCore.Identity;

namespace LeavePlanner.Core.Models.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
