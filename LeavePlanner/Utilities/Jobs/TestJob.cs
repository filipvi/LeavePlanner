using LeavePlanner.Core.Interfaces;
using LeavePlanner.Persistence;

namespace LeavePlanner.Utilities.Jobs
{
    public class TestJob : ITestJob
    {
        readonly IServiceProvider _serviceProvider;

        public TestJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Test()
        {
            try
            {
                using IServiceScope scope = _serviceProvider.CreateScope();
                using ApplicationDbContext ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var test = ctx.TestTables.ToList();
                var count = test.Count;
                Console.WriteLine("Ovo je za test, broj bi trebao biti 121, a dohvaćenih je " + count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}