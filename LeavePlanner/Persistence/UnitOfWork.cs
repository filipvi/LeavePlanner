using LeavePlanner.Core;
using LeavePlanner.Core.Interfaces;
using LeavePlanner.Persistence.Repositories;
using Z.EntityFramework.Plus;

namespace LeavePlanner.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ITestRepository TestRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            TestRepository = new TestRepository(context);
        }

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
