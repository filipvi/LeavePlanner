using LeavePlanner.Core.Models.Identity;

namespace LeavePlanner.Core.Models.Bussiness;

public class LeaveReplacementUser : BaseEntity
{
    public int LeaveId { get; set; }
    public int ApplicationUserId { get; set; }

    public virtual Leave Leave { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }
}