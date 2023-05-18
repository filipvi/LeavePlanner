using AutoMapper;
using LeavePlanner.Core;
using LeavePlanner.Core.Interfaces;
using LeavePlanner.Models.DTOs;
using LeavePlanner.Utilities.HelpData;
using LeavePlanner.Utilities.Security;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace LeavePlanner.Models.ViewModels.Leave
{
    public class IndexLeaveViewModel
    {
        [Display(Name = "Show leaves only for employee")]
        public int? EmployeeId { get; set; }
        public IEnumerable<SelectListItem> EmployeeSelectList { get; set; }
        public List<LeaveDto> Leaves { get; set; }
        public bool CreateEnabled { get; set; }
        public HelpViewModel HelpModel { get; set; }


        public IndexLeaveViewModel()
        {
            EmployeeSelectList = new List<SelectListItem>();
            Leaves = new List<LeaveDto>();
            PrepareHelpData();
        }

        public void PrepareHelpData()
        {
            var helpData = HelpDataFactory.GetHelpData(this);
            HelpModel = helpData.GetHelpData();
        }

        public async Task PrepareSelectLists(IUnitOfWork unitOfWork)
        {
            EmployeeSelectList = await unitOfWork.EmployeeRepository
                    .GetEmployeeSelectListAsync();
        }

        public async Task PrepareHolidays(IMapper mapper, IHolidayService holidayService)
        {
            var publicHolidays = await holidayService.GetHolidaysForCountryAsync();
            mapper.Map(publicHolidays, Leaves);
        }

        public async Task PrepareRequests(IUnitOfWork unitOfWork, IMapper mapper)
        {
            var leaves = await unitOfWork.LeaveRepository.GetEmployeeLeavesAsync(EmployeeId);
            mapper.Map(leaves, Leaves);
        }

        public void PrepareManagementActions(ClaimsPrincipal user)
        {
            CreateEnabled = user.IsInRole(UserRoles.Employee);
        }
    }
}
