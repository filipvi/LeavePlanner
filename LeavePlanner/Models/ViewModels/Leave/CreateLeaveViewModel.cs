﻿using AutoMapper;
using LeavePlanner.Core;
using LeavePlanner.Core.Interfaces;
using LeavePlanner.Core.Models.Bussiness;
using LeavePlanner.Models.Enums;
using LeavePlanner.Models.Exceptions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace LeavePlanner.Models.ViewModels.Leave
{
    public class CreateLeaveViewModel : PartialErrorViewModel
    {
        #region Properties

        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public int RemainingLeaveDaysInYear { get; set; }
        public int WorkingDaysUsed { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }

        [Display(Name = "Replacement employees")]
        [Required(ErrorMessage = "Minimum one employee required")]
        public int[] ReplacementEmployeeIds { get; set; }

        public IEnumerable<SelectListItem> ReplacementEmployeesSelectList { get; set; }


        private readonly IHolidayService _holidayService;
        private static List<DateTime> _holidaysDates;
        private static Task<List<DateTime>> _holidaysTask;

        public CreateLeaveViewModel(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        public CreateLeaveViewModel()
        {

        }

        public async Task<List<DateTime>> GetHolidaysAsync()
        {
            if (_holidaysDates == null)
            {
                _holidaysTask ??= _holidayService.GetHolidaysDatesForCountryAsync();

                _holidaysDates = await _holidaysTask;
            }

            return _holidaysDates;
        }


        public HelpViewModel HelpModel { get; set; }
        public Core.Models.Bussiness.Leave Leave { get; set; }

        #endregion Properties


        public async Task PrepareData(IUnitOfWork unitOfWork, IMapper mapper)
        {
            var isValidFrom = DateTime.TryParseExact(DateFrom,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime startDate);

            var isValidTo = DateTime.TryParseExact(DateTo,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime endDate);

            if (!isValidFrom || !isValidTo)
            {
                throw new CreateNotAllowedException("Date parse error!");
            }

            var currentDate = DateTime.Now;

            if (startDate <= currentDate)
            {
                throw new CreateNotAllowedException("You can choose only dates in future!");
            }

            await GetHolidaysAsync();

            var employee = await unitOfWork.EmployeeRepository
                .GetEmployeeAsync(EmployeeId);

            if (employee == null)
            {
                throw new EntityNotFoundException("Employee not found!");
            }

            mapper.Map(employee, this);



            bool overlapExists = HaveRangeOverlap(employee.Leaves.Where(x => !x.IsDeleted && (x.StatusId == (int)LeaveStatusEnums.Approved || x.StatusId == (int)LeaveStatusEnums.Pending)).ToList(), startDate, endDate);

            if (overlapExists)
            {
                throw new CreateNotAllowedException("You already booked leave in choosen period!");
            }



            int countOfWorkingDays = 0;
            while (startDate < endDate)
            {
                if (startDate.DayOfWeek != DayOfWeek.Saturday && startDate.DayOfWeek != DayOfWeek.Sunday && !_holidaysDates.Contains(startDate))
                {
                    WorkingDaysUsed++;
                }

                startDate = startDate.AddDays(1);
            }

            if (!employee.RemainingLeaveDaysInYear.HasValue ||
                    countOfWorkingDays > employee.RemainingLeaveDaysInYear.Value ||
                    (employee.RemainingLeaveDaysInYear.HasValue &&
                    employee.RemainingLeaveDaysInYear.Value == 0))
            {
                throw new CreateNotAllowedException("You don't have available days!");
            }
        }

        private bool HaveRangeOverlap(List<Core.Models.Bussiness.Leave> leaves, DateTime startDate, DateTime endDate)
        {
            foreach (var leaveRecord in leaves)
            {
                if (DateRangesOverlap(leaveRecord.StartDate, leaveRecord.EndDate, startDate, endDate))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool DateRangesOverlap(DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            return start1 < end2 && start2 < end1;
        }

        public async Task PrepareSelectLists(IUnitOfWork unitOfWork)
        {
            ReplacementEmployeesSelectList = await unitOfWork.EmployeeRepository
                .GetReplacementEmployeeSelectListAsync(EmployeeId);
        }

        public void PrepareDataForSaving(IMapper mapper)
        {
            Leave = new Core.Models.Bussiness.Leave();
            mapper.Map(this, Leave);

            Leave.ReplacementUsers = new List<LeaveReplacementUser>();

            foreach (var replacementEmployeeId in ReplacementEmployeeIds)
            {
                Leave.ReplacementUsers.Add(new LeaveReplacementUser
                {
                    ApplicationUserId = replacementEmployeeId
                });
            }
        }

        public async Task CheckData(IUnitOfWork unitOfWork)
        {
            var employee = await unitOfWork.EmployeeRepository
                .GetEmployeeAsync(EmployeeId);

            var isValidFrom = DateTime.TryParseExact(DateFrom,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime startDate);

            var isValidTo = DateTime.TryParseExact(DateTo,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime endDate);

            if (!isValidFrom || !isValidTo)
            {
                throw new CreateNotAllowedException("Date parse error!");
            }

            bool validDates = HaveRangeOverlap(employee.Leaves.Where(x => !x.IsDeleted && (x.StatusId == (int)LeaveStatusEnums.Approved || x.StatusId == (int)LeaveStatusEnums.Pending)).ToList(), startDate, endDate);

            if (validDates)
            {
                throw new CreateNotAllowedException("You already booked leave in choosen period!");
            }

            int countOfWorkingDays = 0;
            DateTime currentDate = startDate;

            while (currentDate < endDate)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday && !_holidaysDates.Contains(currentDate))
                {
                    countOfWorkingDays++;
                }

                currentDate = currentDate.AddDays(1);
            }

            if (countOfWorkingDays > RemainingLeaveDaysInYear)
            {
                throw new CreateNotAllowedException("You don't have available days!");
            }
        }
    }
}
