using LeavePlanner.Core.Models.Bussiness;

namespace LeavePlanner.Core.Interfaces
{
    public interface ILeaveRepository
    {
        Task<List<Leave>> GetEmployeeLeavesAsync(int? employeeId);
        Task<Leave> GetLeaveAsync(int id);
        Task ApproveAsync(int id);
        Task DeclineAsync(int id);
        Task DeleteAsync(int id);
        Task PendingAsync(int id);
        Task CreateAsync(Leave leave);
    }
}
