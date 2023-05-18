using AutoMapper;
using LeavePlanner.Core;
using LeavePlanner.Models.Exceptions;
using LeavePlanner.Utilities.HelpData;
using LeavePlanner.Utilities.Security;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace LeavePlanner.Models.ViewModels.Employee
{
    public class DetailsEmployeeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "Leave days per year")]
        public string LeaveDaysPerYear { get; set; }

        [Display(Name = "Remaining leave days")]
        public string RemainingLeaveDays { get; set; }

        [Display(Name = "User role")]
        public string UserRole { get; set; }

        public List<DetailsEmployeeLeavesViewModel> Leaves { get; set; }
        public HelpViewModel HelpModel { get; set; }

        public bool DeleteEnabled { get; set; }
        public bool EditEnabled { get; set; }

        public DetailsEmployeeViewModel()
        {
            Leaves = new List<DetailsEmployeeLeavesViewModel>();
            PrepareHelpData();
        }

        private void PrepareHelpData()
        {
            var helpData = HelpDataFactory.GetHelpData(this);
            HelpModel = helpData.GetHelpData();
        }

        public async Task PrepareData(IUnitOfWork unitOfWork, IMapper mapper)
        {
            var employee = await unitOfWork.EmployeeRepository
                .GetEmployeeAsync(Id);

            if (employee == null)
            {
                throw new EntityNotFoundException("Employee not found!");
            }

            mapper.Map(employee, this);

            if (employee.Leaves.Any(x => !x.IsDeleted))
            {
                mapper.Map(employee.Leaves.Where(x => !x.IsDeleted), Leaves);
            }
        }

        public void PrepareManagement(ClaimsPrincipal user)
        {
            if (user.IsInRole(UserRoles.Admin) && UserRole.Equals(UserRoles.Employee, StringComparison.CurrentCultureIgnoreCase))
            {
                DeleteEnabled = true;
                EditEnabled = true;
            }
        }
    }
}
