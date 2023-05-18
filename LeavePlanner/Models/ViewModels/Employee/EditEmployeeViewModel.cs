using AutoMapper;
using LeavePlanner.Core;
using LeavePlanner.Models.Exceptions;
using LeavePlanner.Utilities.HelpData;
using LeavePlanner.Utilities.Security;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LeavePlanner.Models.ViewModels.Employee
{
    public class EditEmployeeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        [Display(Name = "Leave days per year")]
        [RegularExpression("^[1-9][0-9]*$", ErrorMessage = "Must be positive number")]
        public string LeaveDaysPerYear { get; set; }

        [Display(Name = "User role")]
        public int? UserRoleId { get; set; }

        public string UserRole { get; set; }

        public List<SelectListItem> UserRoleSelectList { get; set; }
        public HelpViewModel HelpModel { get; set; }

        public EditEmployeeViewModel()
        {
            UserRoleSelectList = new List<SelectListItem>();
            PrepareHelpData();
        }

        public void PrepareHelpData()
        {
            var helpData = HelpDataFactory.GetHelpData(this);
            HelpModel = helpData.GetHelpData();
        }

        public async Task PrepareData(IUnitOfWork unitOfWork, IMapper mapper)
        {
            var employee = await unitOfWork.EmployeeRepository.GetEmployeeAsync(Id);
            mapper.Map(employee, this);

            if (UserRole.Equals(UserRoles.Admin))
            {
                throw new ChangesNotAllowedException("Cannot edit admin account");
            }
        }

        public async Task PrepareSelectList(IUnitOfWork unitOfWork)
        {
            UserRoleSelectList = await unitOfWork.EmployeeRepository
                .GetUserRoleSelectListAsync();
        }
    }
}
