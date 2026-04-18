using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

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

    public class BannerCarouselService : ServiceBase, IBannerCarouselService
    {
        public BannerCarouselService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("BannerCarousel");

        public async Task<List<Models.BannerCarousel>> GetBannerCarouselsAsync(int ModuleId)
        {
            List<Models.BannerCarousel> BannerCarousels = await GetJsonAsync<List<Models.BannerCarousel>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.BannerCarousel>().ToList());
            return BannerCarousels.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.BannerCarousel> GetBannerCarouselAsync(int BannerCarouselId, int ModuleId)
        {
            return await GetJsonAsync<Models.BannerCarousel>(CreateAuthorizationPolicyUrl($"{Apiurl}/{BannerCarouselId}/{ModuleId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.BannerCarousel> AddBannerCarouselAsync(Models.BannerCarousel BannerCarousel)
        {
            return await PostJsonAsync<Models.BannerCarousel>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, BannerCarousel.ModuleId), BannerCarousel);
        }

        public async Task<Models.BannerCarousel> UpdateBannerCarouselAsync(Models.BannerCarousel BannerCarousel)
        {
            return await PutJsonAsync<Models.BannerCarousel>(CreateAuthorizationPolicyUrl($"{Apiurl}/{BannerCarousel.BannerCarouselId}", EntityNames.Module, BannerCarousel.ModuleId), BannerCarousel);
        }

        public async Task DeleteBannerCarouselAsync(int BannerCarouselId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{BannerCarouselId}/{ModuleId}", EntityNames.Module, ModuleId));
        }
    }
}
