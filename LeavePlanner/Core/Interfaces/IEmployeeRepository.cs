using LeavePlanner.Core.Models.Identity;
using LeavePlanner.Models.ViewModels.Employee;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeavePlanner.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<SelectListItem>> GetEmployeeSelectListAsync();
        Task<IEnumerable<SelectListItem>> GetReplacementEmployeeSelectListAsync(int employeeId);
        IQueryable<ApplicationUser> GetEmployeesQueryable();
        Task<int> GetEmployeesCountAsync();
        Task<ApplicationUser> GetEmployeeAsync(int id);
        Task<List<SelectListItem>> GetUserRoleSelectListAsync();
        Task EditAsync(EditEmployeeViewModel viewModel);
        Task DeleteAsync(int id, UserManager<ApplicationUser> userManager);
        Task<ApplicationUser> SearchEmployeeAsync(string search);
    }
}
