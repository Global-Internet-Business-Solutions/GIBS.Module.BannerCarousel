using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;

namespace GIBS.Module.BannerCarousel.Repository
{
    public interface IBannerCarouselRepository
    {
        IEnumerable<Models.BannerCarousel> GetBannerCarousels(int ModuleId);
        Models.BannerCarousel GetBannerCarousel(int BannerCarouselId);
        Models.BannerCarousel GetBannerCarousel(int BannerCarouselId, bool tracking);
        Models.BannerCarousel AddBannerCarousel(Models.BannerCarousel BannerCarousel);
        Models.BannerCarousel UpdateBannerCarousel(Models.BannerCarousel BannerCarousel);
        void DeleteBannerCarousel(int BannerCarouselId);
    }

    public class BannerCarouselRepository : IBannerCarouselRepository, ITransientService
    {
        private readonly IDbContextFactory<BannerCarouselContext> _factory;

        public BannerCarouselRepository(IDbContextFactory<BannerCarouselContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.BannerCarousel> GetBannerCarousels(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.BannerCarousel.Where(item => item.ModuleId == ModuleId).OrderBy(item => item.OrderBy).ToList();
        }

        public Models.BannerCarousel GetBannerCarousel(int BannerCarouselId)
        {
            return GetBannerCarousel(BannerCarouselId, true);
        }

        public Models.BannerCarousel GetBannerCarousel(int BannerCarouselId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.BannerCarousel.Find(BannerCarouselId);
            }
            else
            {
                return db.BannerCarousel.AsNoTracking().FirstOrDefault(item => item.BannerCarouselId == BannerCarouselId);
            }
        }

        public Models.BannerCarousel AddBannerCarousel(Models.BannerCarousel BannerCarousel)
        {
            using var db = _factory.CreateDbContext();
            db.BannerCarousel.Add(BannerCarousel);
            db.SaveChanges();
            return BannerCarousel;
        }

        public Models.BannerCarousel UpdateBannerCarousel(Models.BannerCarousel BannerCarousel)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(BannerCarousel).State = EntityState.Modified;
            db.SaveChanges();
            return BannerCarousel;
        }

        public void DeleteBannerCarousel(int BannerCarouselId)
        {
            using var db = _factory.CreateDbContext();
            Models.BannerCarousel BannerCarousel = db.BannerCarousel.Find(BannerCarouselId);
            db.BannerCarousel.Remove(BannerCarousel);
            db.SaveChanges();
        }
    }
}
