using LeavePlanner.Core.Interfaces;
using Z.EntityFramework.Plus;

namespace LeavePlanner.Core
{
    public interface IUnitOfWork
    {
        IEmployeeRepository EmployeeRepository { get; }
        ILeaveRepository LeaveRepository { get; }

        Task CompleteAsync();
        Task CompleteAsync(Audit audit);

        void Complete();
        void Complete(Audit audit);
    }
}
