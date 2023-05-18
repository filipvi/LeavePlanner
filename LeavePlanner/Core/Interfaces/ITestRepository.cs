

using LeavePlanner.Core.Models.Test;

namespace LeavePlanner.Core.Interfaces
{
    public interface ITestRepository
    {
        Task<List<TestTable>> GetTestDataAsync();
    }
}
