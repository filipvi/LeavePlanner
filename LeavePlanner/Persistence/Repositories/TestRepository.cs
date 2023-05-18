
using LeavePlanner.Core.Interfaces;
using LeavePlanner.Core.Models.Test;
using Microsoft.EntityFrameworkCore;

namespace LeavePlanner.Persistence.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext _context;

        public TestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TestTable>> GetTestDataAsync()
        {
            var testData = await _context.TestTables.ToListAsync();

            return testData;
        }
    }
}
