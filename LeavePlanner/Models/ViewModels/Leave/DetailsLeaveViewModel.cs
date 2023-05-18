using AutoMapper;
using LeavePlanner.Core;
using LeavePlanner.Utilities.Security;
using System.Security.Claims;

namespace LeavePlanner.Models.ViewModels.Leave
{
    public class DetailsLeaveViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Employee { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int WorkingDays { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public List<string> ReplacementEmployees { get; set; }
        public bool ChangeStatusEnabled { get; set; }
        public bool DeleteEnabled { get; set; }

        public async Task PrepareData(IUnitOfWork unitOfWork, IMapper mapper)
        {
            var leave = await unitOfWork.LeaveRepository
                                          .GetLeaveAsync(Id);

            mapper.Map(leave, this);
        }

        public void PrepareManagementActions(ClaimsPrincipal user)
        {
            ChangeStatusEnabled = DateFrom.Date > DateTime.Now.Date && user.IsInRole(UserRoles.Admin);
            DeleteEnabled = DateFrom.Date > DateTime.Now.Date;

        }
    }
}
