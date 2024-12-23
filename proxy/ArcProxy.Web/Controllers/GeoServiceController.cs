using ArcProxy.Core.Services;
using ArcProxy.Web.Extensions;
using ArcProxy.Web.Filters;
using AspNetCore.Proxy;
using Microsoft.AspNetCore.Mvc;

namespace ArcProxy.Web.Controllers
{
    [TypeFilter<RestExceptionFilter>]
    [Route("/arcservertest/rest/services/{**serviceUri}")]
    public class GeoServiceController : Controller
    {
        private readonly IGeoService _geoService;
        private readonly IConfiguration _config;
        public GeoServiceController(IGeoService geoService, IConfiguration config)
        {
            _geoService = geoService;
            _config = config;
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
            await this.HttpProxyAsync($"{_config["ProxiedUrl"]}/{servicePath}");
        }
    }
}
