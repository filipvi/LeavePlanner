using LeavePlanner.Core;

namespace LeavePlanner.Models.ViewModels.Employee
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public int EmployeeId { get; set; }

        public async Task GetEmployee(IUnitOfWork unitOfWork)
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                var employee = await unitOfWork.EmployeeRepository.SearchEmployeeAsync(SearchTerm.Trim().ToLower());
                EmployeeId = employee.Id;
            }
        }
    }
}
