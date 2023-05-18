using AutoMapper;
using LeavePlanner.Core.Models.Bussiness;
using LeavePlanner.Core.Models.Identity;
using LeavePlanner.Models.DTOs;
using LeavePlanner.Models.Enums;
using LeavePlanner.Models.ViewModels.Leave;
using LeavePlanner.Utilities.Extensions;

namespace LeavePlanner.Mapping
{
    public class LeaveProfile : Profile
    {
        public LeaveProfile()
        {
            #region Leave, Leave DTO

            CreateMap<Leave, LeaveDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.Name))
               .ForMember(d => d.WorkingDays, opt => opt.MapFrom(s => s.WorkingDaysUsed))
               .ForMember(d => d.Title, opt => opt.MapFrom(s => s.ApplicationUser.MapUserFullName() + " (" + s.Status.Name + ")"))
               .ForMember(d => d.Start, opt => opt.MapFrom(s => s.StartDate.ToString("yyyy-MM-ddTHH:mm:ss")))
               .ForMember(d => d.End, opt => opt.MapFrom(s => s.EndDate.ToString("yyyy-MM-ddTHH:mm:ss")))
               .ForMember(d => d.BackgroundColor, opt => opt.MapFrom(s => s.MapEventBackgroundColor()));

            #endregion Leave, Leave DTO

            #region HolidayDto, LeaveDto

            CreateMap<HolidayDto, LeaveDto>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.Status, opt => opt.Ignore())
                .ForMember(d => d.WorkingDays, opt => opt.Ignore())
                .ForMember(d => d.Title, opt => opt.MapFrom(s => s.LocalName))
                .ForMember(d => d.Start, opt => opt.MapFrom(s => s.Date.ToString("yyyy-MM-ddTHH:mm:ss")))
                .ForMember(d => d.End, opt => opt.MapFrom(s => s.Date.ToString("yyyy-MM-ddTHH:mm:ss")))
                .ForMember(d => d.BackgroundColor, opt => opt.MapFrom(s => "#dfbf7b"));

            #endregion HolidayDto, LeaveDto

            #region Leave, DetailsLeaveViewModel

            CreateMap<Leave, DetailsLeaveViewModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.StatusId, opt => opt.MapFrom(s => s.StatusId))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.Name))
                .ForMember(d => d.WorkingDays, opt => opt.MapFrom(s => s.WorkingDaysUsed))
                .ForMember(d => d.DateFrom, opt => opt.MapFrom(s => s.StartDate.Date))
                .ForMember(d => d.DateTo, opt => opt.MapFrom(s => s.EndDate.Date))
                .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.ApplicationUserId))
                .ForMember(d => d.Employee, opt => opt.MapFrom(s => s.ApplicationUser.MapUserFullName()))
                .ForMember(d => d.ReplacementEmployees, opt => opt.MapFrom(s => s.MapReplacementEmployees()));

            #endregion Leave, DetailsLeaveViewModel

            #region ApplicationUser, CreateLeaveViewModel

            CreateMap<ApplicationUser, CreateLeaveViewModel>()
                .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.MapUserFullName()))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(d => d.UserRole, opt => opt.MapFrom(s => s.UserRoles.FirstOrDefault().Role.Name))
                .ForMember(d => d.RemainingLeaveDaysInYear, opt => opt.MapFrom(s => s.MapRemainingLeaveDaysInCurrentYear()));

            #endregion ApplicationUser, CreateLeaveViewModel

            #region CreateLeaveViewModel, Leave

            CreateMap<CreateLeaveViewModel, Leave>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.WorkingDaysUsed, opt => opt.MapFrom(s => s.WorkingDaysUsed))
                .ForMember(d => d.ApplicationUserId, opt => opt.MapFrom(s => s.EmployeeId))
                .ForMember(d => d.StatusId, opt => opt.MapFrom(s => (int)LeaveStatusEnums.Pending))
                .ForMember(d => d.StartDate, opt => opt.MapFrom(s => s.DateFrom.StringToDateTime()))
                .ForMember(d => d.EndDate, opt => opt.MapFrom(s => s.DateTo.StringToDateTime()))
                .ForMember(d => d.ReplacementUsers, opt => opt.Ignore());

            #endregion CreateLeaveViewModel, Leave
        }
    }
}
