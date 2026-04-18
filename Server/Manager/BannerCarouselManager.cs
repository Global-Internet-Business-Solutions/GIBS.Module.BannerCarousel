using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using GIBS.Module.BannerCarousel.Repository;
using System.Threading.Tasks;

namespace GIBS.Module.BannerCarousel.Manager
{
    public class BannerCarouselManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly IBannerCarouselRepository _BannerCarouselRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public BannerCarouselManager(IBannerCarouselRepository BannerCarouselRepository, IDBContextDependencies DBContextDependencies)
        {
            _BannerCarouselRepository = BannerCarouselRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new BannerCarouselContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new BannerCarouselContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.BannerCarousel> BannerCarousels = _BannerCarouselRepository.GetBannerCarousels(module.ModuleId).ToList();
            if (BannerCarousels != null)
            {
                content = JsonSerializer.Serialize(BannerCarousels);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.BannerCarousel> BannerCarousels = null;
            if (!string.IsNullOrEmpty(content))
            {
                BannerCarousels = JsonSerializer.Deserialize<List<Models.BannerCarousel>>(content);
            }
            if (BannerCarousels != null)
            {
                foreach(var BannerCarousel in BannerCarousels)
                {
                    _BannerCarouselRepository.AddBannerCarousel(new Models.BannerCarousel { ModuleId = module.ModuleId, Title = BannerCarousel.Title });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var BannerCarousel in _BannerCarouselRepository.GetBannerCarousels(pageModule.ModuleId))
           {
               if (BannerCarousel.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "GIBSBannerCarousel",
                       EntityId = BannerCarousel.BannerCarouselId.ToString(),
                       Title = BannerCarousel.Title,
                       Body = BannerCarousel.Title,
                       ContentModifiedBy = BannerCarousel.ModifiedBy,
                       ContentModifiedOn = BannerCarousel.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
