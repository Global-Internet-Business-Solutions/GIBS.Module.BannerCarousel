using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIBS.Module.BannerCarousel.Services
{
    public interface IBannerCarouselService 
    {
        Task<List<Models.BannerCarousel>> GetBannerCarouselsAsync(int ModuleId);

        Task<Models.BannerCarousel> GetBannerCarouselAsync(int BannerCarouselId, int ModuleId);

        Task<Models.BannerCarousel> AddBannerCarouselAsync(Models.BannerCarousel BannerCarousel);

        Task<Models.BannerCarousel> UpdateBannerCarouselAsync(Models.BannerCarousel BannerCarousel);

        Task DeleteBannerCarouselAsync(int BannerCarouselId, int ModuleId);
    }
}
