using LeavePlanner.Core.Models.Codex;
using LeavePlanner.Core.Models.Identity;

namespace LeavePlanner.Core.Models.Bussiness
{
    public class Leave : BaseEntity
    {
        public int ApplicationUserId { get; set; }
        public int StatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int WorkingDaysUsed { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual LeaveStatus Status { get; set; }
        public virtual List<LeaveReplacementUser> ReplacementUsers { get; set; }


    }
}
