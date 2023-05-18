using LeavePlanner.Core.Interfaces;
using LeavePlanner.Core.Models.Identity;
using LeavePlanner.Models;
using LeavePlanner.Models.Exceptions;
using LeavePlanner.Models.ViewModels.Employee;
using LeavePlanner.Utilities.Extensions;
using LeavePlanner.Utilities.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeavePlanner.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetEmployeeSelectListAsync()
        {
            var employees = await _context.ApplicationUsers
                .Include(x => x.UserRoles)
                .Where(x => x.UserRoles.Any() &&
                x.UserRoles.All(y => y.RoleId == (int)UserRoleEnums.Employee))
                .ToListAsync();

            return employees.ToSelectList(x => x.Id, x => x.MapUserFullName());
        }

        public async Task<IEnumerable<SelectListItem>> GetReplacementEmployeeSelectListAsync(int employeeId)
        {
            var employees = await _context.ApplicationUsers
                .Include(x => x.UserRoles)
                .Where(x => x.Id != employeeId && x.UserRoles.Any() && x.UserRoles.All(y => y.RoleId == (int)UserRoleEnums.Employee))
                .ToListAsync();

            return employees.ToSelectList(x => x.Id, x => x.MapUserFullName());
        }

        public IQueryable<ApplicationUser> GetEmployeesQueryable()
        {
            return _context.ApplicationUsers
                .Include(x => x.UserRoles).ThenInclude(y => y.Role);
        }

        public async Task<int> GetEmployeesCountAsync()
        {
            return await _context.ApplicationUsers.CountAsync();
        }

        public async Task<ApplicationUser> GetEmployeeAsync(int id)
        {
            return await _context.ApplicationUsers
                .Include(x => x.UserRoles).ThenInclude(y => y.Role)
                .Include(x => x.Leaves).ThenInclude(x => x.Status)
                .SingleOrDefaultAsync(x => x.Id == id);

        }

        public async Task<List<SelectListItem>> GetUserRoleSelectListAsync()
        {
            var userRoles = await _context.Roles
                .ToListAsync();

            return userRoles.ToSelectList(x => x.Id, x => x.Name);
        }

        public async Task EditAsync(EditEmployeeViewModel viewModel)
        {
            var employee = await GetEmployeeAsync(viewModel.Id);

            if (!string.IsNullOrWhiteSpace(viewModel.LeaveDaysPerYear))
            {
                bool valid = int.TryParse(viewModel.LeaveDaysPerYear, out int days);
                if (valid)
                {
                    employee.LeaveDaysPerYear = days;
                }
            }

            if (viewModel.UserRoleId.HasValue)
            {
                var role = employee.UserRoles.FirstOrDefault();

                if (role != null)
                {
                    employee.UserRoles.Remove(role);
                }

                employee.UserRoles.Add(new ApplicationUserRole
                {
                    RoleId = viewModel.UserRoleId.Value,
                    UserId = viewModel.Id
                });
            }
            else
            {
                if (employee.UserRoles.Any())
                {
                    var role = employee.UserRoles.FirstOrDefault();
                    employee.UserRoles.Remove(role);
                }
            }
        }

        public async Task DeleteAsync(int id, UserManager<ApplicationUser> userManager)
        {
            var employee = await GetEmployeeAsync(id);
            var roles = await userManager.GetRolesAsync(employee);

            if (roles.Any(x => x.Equals(UserRoles.Admin, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw new DeleteNotAllowedException("Cannot delete admin account");
            }
            await userManager.RemoveFromRolesAsync(employee, roles.ToArray());

            foreach (var leave in employee.Leaves)
            {
                leave.IsDeleted = true;
            }

            await userManager.DeleteAsync(employee);
        }

        public async Task<string> GetMaxDateForEmployeeAsync(int employeeId, string dateFrom,
            IHolidayService holidayService)
        {
            var holidays = await holidayService.GetHolidaysForCountryAsync();
            var holidayDates = holidays.Select(x => x.Date).Distinct().ToList();
            var employee = await GetEmployeeAsync(employeeId);

            DateTime startDate = dateFrom.StringToDateTime();
            DateTime endDate = PrepareEndDate(startDate, employee.RemainingLeaveDaysInYear.Value, holidayDates);

            return endDate.DateToString();
        }


        private static DateTime PrepareEndDate(DateTime startDate, int remaining, List<DateTime> holidayDates)
        {
            DateTime currentDate = startDate;
            int daysAdded = 0;

            while (daysAdded < remaining)
            {
                currentDate = currentDate.AddDays(1);

                if (currentDate.DayOfWeek != DayOfWeek.Saturday && currentDate.DayOfWeek != DayOfWeek.Sunday && !holidayDates.Contains(currentDate))
                {
                    daysAdded++;
                }
            }

            return currentDate;
        }

        public async Task<ApplicationUser> SearchEmployeeAsync(string search)
        {
            var employee = await _context.ApplicationUsers
                .FirstOrDefaultAsync(x =>
                    search.Contains(x.FirstName.Trim().ToLower()) || search.Contains(x.LastName.Trim().ToLower()));

            if (employee == null)
            {
                throw new EntityNotFoundException("Employee not found!");
            }
            return employee;
        }

    }
}
