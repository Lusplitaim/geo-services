using ArcProxy.Core.Data;
using ArcProxy.Core.Models;
using Microsoft.Extensions.Caching.Memory;

namespace ArcProxy.Core.Services
{
    internal class GeoService : IGeoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;
        public GeoService(IUnitOfWork uow, IMemoryCache memoryCache)
        {
            _unitOfWork = uow;
            _memoryCache = memoryCache;
        }

        public async Task<bool> TryAccessAsync(string serviceUri)
        {
            try
            {
                var cachedStat = await _memoryCache.GetOrCreateAsync(serviceUri, async entry =>
                {
                    var geoServiceEntity = await _unitOfWork.GeoServiceRepository.GetAsync(serviceUri);
                    var result = new CachedGeoServiceStat()
                    {
                        ServiceUri = serviceUri,
                        RequestLimit = geoServiceEntity?.Rule?.RequestLimit ?? 0,
                        IsServiceFree = geoServiceEntity is null,
                    };
                    entry.Value = result;
                    return result;
                });

                bool isAllowedToAccess = cachedStat!.IsServiceFree
                    || (cachedStat.RequestCount < cachedStat.RequestLimit);

                if (isAllowedToAccess)
                {
                    cachedStat.RequestCount++;
                    _memoryCache.Set(serviceUri, cachedStat);
                }

                return isAllowedToAccess;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get geo service permission", ex);
            }
        }
    }
}
