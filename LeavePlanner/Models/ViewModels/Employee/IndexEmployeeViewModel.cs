using AutoMapper;
using LeavePlanner.Core;
using LeavePlanner.Core.Models.Identity;
using LeavePlanner.Models.AuxiliaryModels;
using LeavePlanner.Models.DTOs;
using LeavePlanner.Utilities.HelpData;
using Microsoft.EntityFrameworkCore;

namespace LeavePlanner.Models.ViewModels.Employee
{
    public class IndexEmployeeViewModel : DataTableProperties
    {
        public List<EmployeeDto> Employees { get; set; }
        public HelpViewModel HelpModel { get; set; }

        public IndexEmployeeViewModel()
        {
            PrepareHelpData();
        }

        private void PrepareHelpData()
        {
            var helpData = HelpDataFactory.GetHelpData(this);
            HelpModel = helpData.GetHelpData();
        }

        public async Task GetData(IFormCollection form, IUnitOfWork unitOfWork, IMapper mapper)
        {
            ExtractDataTableProperties(form);

            var employeesQueryable = unitOfWork.EmployeeRepository
                .GetEmployeesQueryable();
            TotalRecords = await unitOfWork.EmployeeRepository
                .GetEmployeesCountAsync();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                Search = Search.Trim().ToLower();
                employeesQueryable = ApplySearch(employeesQueryable);
            }

            // sorting
            employeesQueryable = SortByColumnWithOrder(employeesQueryable);

            // count filtered data
            RecFilter = employeesQueryable.Count();

            // pagination
            var employeesList = await employeesQueryable.Skip(StartRec).Take(PageSize).ToListAsync();

            // map data
            Employees = mapper.Map<List<ApplicationUser>, List<EmployeeDto>>(employeesList);
        }

        private IQueryable<ApplicationUser> SortByColumnWithOrder(IQueryable<ApplicationUser> employees)
        {
            // sorting
            switch (Order)
            {
                case "0":
                    employees = OrderDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase)
                        ? employees.OrderBy(p => p.LastName).ThenBy(p => p.FirstName)
                        : employees.OrderByDescending(p => p.LastName).ThenByDescending(p => p.FirstName);
                    break;
                case "3":
                    employees = OrderDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase)
                        ? employees.OrderBy(p => p.LeaveDaysPerYear).ThenBy(p => p.LeaveDaysPerYear)
                        : employees.OrderByDescending(p => p.LeaveDaysPerYear).ThenByDescending(p => p.LeaveDaysPerYear);
                    break;
            }

            return employees;
        }

        private IQueryable<ApplicationUser> ApplySearch(IQueryable<ApplicationUser> employeesQueryable)
        {
            employeesQueryable = employeesQueryable.Where(x =>
                x.FirstName.Trim().ToLower().Contains(Search) ||
                x.LastName.Trim().ToLower().Contains(Search) ||
                x.Email.Trim().ToLower().Contains(Search) ||
                (x.UserRoles.Any() && x.UserRoles.FirstOrDefault().Role.Name.Trim().ToLower().Contains(Search)));

            return employeesQueryable;
        }

        private void ExtractDataTableProperties(IFormCollection form)
        {
            Search = form["search[value]"][0];
            Draw = form["draw"][0];
            Order = form["order[0][column]"][0];
            OrderDir = form["order[0][dir]"][0];
            StartRec = Convert.ToInt32(form["start"][0]);
            PageSize = Convert.ToInt32(form["length"][0]);
        }
    }
}
