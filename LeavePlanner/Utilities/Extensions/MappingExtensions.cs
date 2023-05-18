#region

using LeavePlanner.Core.Models.Bussiness;
using LeavePlanner.Core.Models.Identity;
using LeavePlanner.Models.Enums;

#endregion

namespace LeavePlanner.Utilities.Extensions
{
    public static class MappingExtensions
    {
        public static int MapRemainingLeaveDaysInCurrentYear(this ApplicationUser user)
        {
            int remainingDaysCurrentYear = 0;
            var currentYear = DateTime.Now.Year;

            if (user == null || !user.LeaveDaysPerYear.HasValue || user.LeaveDaysPerYear.Value == 0)
            {
                return remainingDaysCurrentYear;
            }

            var existingLeaves = user.Leaves
                .Where(x => !x.IsDeleted && x.StatusId == (int)LeaveStatusEnums.Approved && x.StartDate.Year == currentYear);
            return user.LeaveDaysPerYear.Value - existingLeaves.Sum(x => x.WorkingDaysUsed);
        }

        public static string MapUserFullName(this ApplicationUser user)
        {
            string fullName = string.Empty;

            if (user == null)
            {
                return fullName;
            }

            fullName = user.FirstName + " " + user.LastName;
            return fullName;
        }

        public static string MapUserRole(this ApplicationUser user)
        {
            string role = string.Empty;

            if (user == null || !user.UserRoles.Any() || user.UserRoles.FirstOrDefault()?.Role == null)
            {
                return role;
            }

            role = user.UserRoles.FirstOrDefault().Role.Name;
            return role;
        }

        public static int? MapUserRoleId(this ApplicationUser user)
        {
            int? roleId = null;

            if (user == null || !user.UserRoles.Any() || user.UserRoles.FirstOrDefault()?.Role == null)
            {
                return roleId;
            }

            roleId = user.UserRoles.FirstOrDefault().Role.Id;
            return roleId;
        }

        public static string MapEventBackgroundColor(this Leave leave)
        {
            string bgColor = string.Empty;

            if (leave == null || leave.Status == null)
            {
                return bgColor;
            }

            if (leave.EndDate.Date < DateTime.Now.Date)
            {
                return "#9ea7af"; // past
            }

            if (leave.StatusId == (int)LeaveStatusEnums.Pending)
            {
                bgColor = "#70a3cb"; // past

            }
            else if (leave.StatusId == (int)LeaveStatusEnums.Declined)
            {
                bgColor = "#cd7c7c"; // past

            }
            else if (leave.StatusId == (int)LeaveStatusEnums.Approved)
            {
                bgColor = "#56ad65"; // past
            }

            return bgColor;
        }

        public static List<string> MapReplacementEmployees(this Leave leave)
        {
            List<string> names = new List<string>();

            if (leave.ReplacementUsers == null)
            {
                return names;
            }

            names.AddRange(leave.ReplacementUsers.Select(x => x.ApplicationUser.MapUserFullName()));

            return names;
        }
    }
}