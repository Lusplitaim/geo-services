using ArcProxy.Core.Extensions;
using ArcProxy.Core.Models.DTOs;
using ArcProxy.Core.Services;
using ArcProxy.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ArcProxy.Web.Controllers
{
    [ApiController]
    [TypeFilter<RestExceptionFilter>]
    [Route("[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IGeoService _geoService;
        public ServicesController(IGeoService geoService)
        {
            _geoService = geoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var result = await _geoService.GetAsync();
            return Ok(result);
        }

        [HttpPut("{serviceId}")]
        public async Task<IActionResult> UpdateService(int serviceId, GeoServiceUpdateDto model)
        {
            var result = await _geoService.UpdateAsync(serviceId, model);
            return this.ResolveResult(result, () => CreatedAtAction(nameof(UpdateService), result.Result));
        }
    }
}
