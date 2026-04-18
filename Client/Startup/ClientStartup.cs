using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Oqtane.Services;
using GIBS.Module.BannerCarousel.Services;

namespace GIBS.Module.BannerCarousel.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            if (!services.Any(s => s.ServiceType == typeof(IBannerCarouselService)))
            {
                services.AddScoped<IBannerCarouselService, ClientBannerCarouselService>();
            }
        }
    }
}
