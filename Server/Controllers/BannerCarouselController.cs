using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using GIBS.Module.BannerCarousel.Services;
using Oqtane.Controllers;
using System.Net;
using System.Threading.Tasks;

namespace GIBS.Module.BannerCarousel.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class BannerCarouselController : ModuleControllerBase
    {
        private readonly IBannerCarouselService _BannerCarouselService;

        public BannerCarouselController(IBannerCarouselService BannerCarouselService, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _BannerCarouselService = BannerCarouselService;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<Models.BannerCarousel>> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return await _BannerCarouselService.GetBannerCarouselsAsync(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized BannerCarousel Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<Models.BannerCarousel> Get(int id, int moduleid)
        {
            Models.BannerCarousel BannerCarousel = await _BannerCarouselService.GetBannerCarouselAsync(id, moduleid);
            if (BannerCarousel != null && IsAuthorizedEntityId(EntityNames.Module, BannerCarousel.ModuleId))
            {
                return BannerCarousel;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized BannerCarousel Get Attempt {BannerCarouselId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.BannerCarousel> Post([FromBody] Models.BannerCarousel BannerCarousel)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, BannerCarousel.ModuleId))
            {
                BannerCarousel = await _BannerCarouselService.AddBannerCarouselAsync(BannerCarousel);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized BannerCarousel Post Attempt {BannerCarousel}", BannerCarousel);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                BannerCarousel = null;
            }
            return BannerCarousel;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.BannerCarousel> Put(int id, [FromBody] Models.BannerCarousel BannerCarousel)
        {
            if (ModelState.IsValid && BannerCarousel.BannerCarouselId == id && IsAuthorizedEntityId(EntityNames.Module, BannerCarousel.ModuleId))
            {
                BannerCarousel = await _BannerCarouselService.UpdateBannerCarouselAsync(BannerCarousel);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized BannerCarousel Put Attempt {BannerCarousel}", BannerCarousel);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                BannerCarousel = null;
            }
            return BannerCarousel;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task Delete(int id, int moduleid)
        {
            Models.BannerCarousel BannerCarousel = await _BannerCarouselService.GetBannerCarouselAsync(id, moduleid);
            if (BannerCarousel != null && IsAuthorizedEntityId(EntityNames.Module, BannerCarousel.ModuleId))
            {
                await _BannerCarouselService.DeleteBannerCarouselAsync(id, BannerCarousel.ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized BannerCarousel Delete Attempt {BannerCarouselId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
