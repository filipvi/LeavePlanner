using LeavePlanner.Core;
using LeavePlanner.Core.Interfaces;
using LeavePlanner.Persistence.Repositories;
using LeavePlanner.Utilities.Settings;
using Microsoft.Extensions.Options;
using Z.EntityFramework.Plus;

namespace LeavePlanner.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IOptions<HolidayApi> _holidaysApi;


        public UnitOfWork(ApplicationDbContext context, IOptions<HolidayApi> holidaysApi)
        {
            _context = context;
            _holidaysApi = holidaysApi;

            EmployeeRepository = new EmployeeRepository(context);
            LeaveRepository = new LeaveRepository(context);

        }

        public IEmployeeRepository EmployeeRepository { get; }
        public ILeaveRepository LeaveRepository { get; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CompleteAsync(Audit audit)
        {
            await _context.SaveChangesAsync(audit);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Complete(Audit audit)
        {
            _context.SaveChanges(audit);
        }
    }
}
