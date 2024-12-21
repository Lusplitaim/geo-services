using ArcProxy.Core.Services;
using ArcProxy.Web.Extensions;
using AspNetCore.Proxy;
using Microsoft.AspNetCore.Mvc;

namespace ArcProxy.Web.Controllers
{
    [ApiController]
    [Route("/arcservertest/rest/services/{**serviceUri}")]
    public class GeoServiceController : ControllerBase
    {
        private readonly IGeoService _geoService;
        public GeoServiceController(IGeoService geoService)
        {
            _geoService = geoService;
        }

        [HttpGet]
        public async Task GetService(string serviceUri)
        {
            var isAvailable = await _geoService.TryAccessAsync(serviceUri);
            if (!isAvailable)
            {
                this.Forbidden();
                return;
            }

            var servicePath = serviceUri;
            if (Request.QueryString.HasValue)
            {
                servicePath += Request.QueryString.Value;
            }
            await this.HttpProxyAsync($"https://portaltest.gismap.by/arcservertest/rest/services/{servicePath}");
        }
    }
}
