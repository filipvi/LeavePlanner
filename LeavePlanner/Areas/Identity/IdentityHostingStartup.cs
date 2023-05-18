[assembly: HostingStartup(typeof(LeavePlanner.Areas.Identity.IdentityHostingStartup))]
namespace LeavePlanner.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
