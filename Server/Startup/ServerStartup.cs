using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using GIBS.Module.BannerCarousel.Repository;
using GIBS.Module.BannerCarousel.Services;

namespace GIBS.Module.BannerCarousel.Startup
{
    public class ServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBannerCarouselService, ServerBannerCarouselService>();
            services.AddDbContextFactory<BannerCarouselContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
