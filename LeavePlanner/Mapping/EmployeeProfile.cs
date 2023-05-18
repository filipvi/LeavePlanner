using AutoMapper;
using LeavePlanner.Core.Models.Bussiness;
using LeavePlanner.Core.Models.Identity;
using LeavePlanner.Models.DTOs;
using LeavePlanner.Models.ViewModels.Employee;
using LeavePlanner.Utilities.Extensions;

namespace LeavePlanner.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<ApplicationUser, EmployeeDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.MapUserFullName()))
                .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(d => d.UserRole, opt => opt.MapFrom(s => s.MapUserRole()))
                .ForMember(d => d.LeaveDaysPerYear, opt => opt.MapFrom(s => s.LeaveDaysPerYear.HasValue ? s.LeaveDaysPerYear.Value : 0))
                .ForMember(d => d.RemainingDaysCurrentYear, opt => opt.MapFrom(s => s.MapRemainingLeaveDaysInCurrentYear()));

            CreateMap<ApplicationUser, DetailsEmployeeViewModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(d => d.LeaveDaysPerYear, opt => opt.MapFrom(s => s.LeaveDaysPerYear.HasValue ? s.LeaveDaysPerYear.Value.ToString() : string.Empty))
                .ForMember(d => d.RemainingDaysCurrentYear, opt => opt.MapFrom(s => s.MapRemainingLeaveDaysInCurrentYear()))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(d => d.UserRole, opt => opt.MapFrom(s => s.MapUserRole()))
                .ForMember(d => d.Leaves, opt => opt.Ignore());

            CreateMap<Leave, DetailsEmployeeLeavesViewModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.DateFrom, opt => opt.MapFrom(s => s.StartDate.ToShortDateString()))
                .ForMember(d => d.DateTo, opt => opt.MapFrom(s => s.EndDate.ToShortDateString()))
                .ForMember(d => d.WorkingDaysUsed, opt => opt.MapFrom(s => s.WorkingDaysUsed))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.Name))
                .ForMember(d => d.StatusId, opt => opt.MapFrom(s => s.StatusId));

            CreateMap<ApplicationUser, EditEmployeeViewModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.MapUserFullName()))
                .ForMember(d => d.LeaveDaysPerYear,
                    opt => opt.MapFrom(s =>
                        s.LeaveDaysPerYear.HasValue ? s.LeaveDaysPerYear.Value.ToString() : string.Empty))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(d => d.UserRole, opt => opt.MapFrom(s => s.MapUserRole()))
                .ForMember(d => d.UserRoleId, opt => opt.MapFrom(s => s.MapUserRoleId()));
        }
    }
}
