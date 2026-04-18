using Oqtane.Models;
using Oqtane.Modules;

namespace GIBS.Module.BannerCarousel
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "BannerCarousel",
            Description = "Banner Carousel Module for Oqtane",
            Version = "1.0.1",
            ServerManagerType = "GIBS.Module.BannerCarousel.Manager.BannerCarouselManager, GIBS.Module.BannerCarousel.Server.Oqtane",
            ReleaseVersions = "1.0.0,1.0.1",
            Dependencies = "GIBS.Module.BannerCarousel.Shared.Oqtane",
            PackageName = "GIBS.Module.BannerCarousel" 
        };
    }
}
