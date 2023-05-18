using LeavePlanner.Core.Models.Bussiness;
using Microsoft.AspNetCore.Identity;

namespace LeavePlanner.Core.Models.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public override int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? LeaveDaysPerYear { get; set; }
        public int? RemainingLeaveDaysInYear { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<Leave> Leaves { get; set; }
    }
}
