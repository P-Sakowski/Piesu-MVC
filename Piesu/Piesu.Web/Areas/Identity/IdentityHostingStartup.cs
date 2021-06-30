using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Piesu.Web.Areas.Identity.IdentityHostingStartup))]
namespace Piesu.Web.Areas.Identity
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