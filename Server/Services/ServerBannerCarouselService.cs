using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using GIBS.Module.BannerCarousel.Repository;

namespace GIBS.Module.BannerCarousel.Services
{
    public class ServerBannerCarouselService : IBannerCarouselService
    {
        private readonly IBannerCarouselRepository _BannerCarouselRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerBannerCarouselService(IBannerCarouselRepository BannerCarouselRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _BannerCarouselRepository = BannerCarouselRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.BannerCarousel>> GetBannerCarouselsAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_BannerCarouselRepository.GetBannerCarousels(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized BannerCarousel Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.BannerCarousel> GetBannerCarouselAsync(int BannerCarouselId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_BannerCarouselRepository.GetBannerCarousel(BannerCarouselId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized BannerCarousel Get Attempt {BannerCarouselId} {ModuleId}", BannerCarouselId, ModuleId);
                return null;
            }
        }

        public Task<Models.BannerCarousel> AddBannerCarouselAsync(Models.BannerCarousel BannerCarousel)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, BannerCarousel.ModuleId, PermissionNames.Edit))
            {
                BannerCarousel = _BannerCarouselRepository.AddBannerCarousel(BannerCarousel);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "BannerCarousel Added {BannerCarousel}", BannerCarousel);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized BannerCarousel Add Attempt {BannerCarousel}", BannerCarousel);
                BannerCarousel = null;
            }
            return Task.FromResult(BannerCarousel);
        }

        public Task<Models.BannerCarousel> UpdateBannerCarouselAsync(Models.BannerCarousel BannerCarousel)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, BannerCarousel.ModuleId, PermissionNames.Edit))
            {
                BannerCarousel = _BannerCarouselRepository.UpdateBannerCarousel(BannerCarousel);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "BannerCarousel Updated {BannerCarousel}", BannerCarousel);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized BannerCarousel Update Attempt {BannerCarousel}", BannerCarousel);
                BannerCarousel = null;
            }
            return Task.FromResult(BannerCarousel);
        }

        public Task DeleteBannerCarouselAsync(int BannerCarouselId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _BannerCarouselRepository.DeleteBannerCarousel(BannerCarouselId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "BannerCarousel Deleted {BannerCarouselId}", BannerCarouselId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized BannerCarousel Delete Attempt {BannerCarouselId} {ModuleId}", BannerCarouselId, ModuleId);
            }
            return Task.CompletedTask;
        }
    }
}
