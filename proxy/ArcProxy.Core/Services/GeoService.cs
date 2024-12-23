using ArcProxy.Core.Data;
using ArcProxy.Core.Exceptions;
using ArcProxy.Core.Models;
using ArcProxy.Core.Models.DTOs;
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
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to access geo-service", ex);
            }
        }

        public async Task<IEnumerable<GeoServiceDto>> GetAsync()
        {
            try
            {
                var services = await _unitOfWork.GeoServiceRepository.GetAsync();

                var result = new List<GeoServiceDto>();

                foreach (var service in services)
                {
                    var serviceStat = _memoryCache.Get<CachedGeoServiceStat>(service.Uri);
                    result.Add(new()
                    {
                        Id = service.Id,
                        Name = service.Name!,
                        ServiceUri = service.Uri,
                        RequestLimit = service?.Rule?.RequestLimit ?? 0,
                        RequestCount = serviceStat?.RequestCount ?? 0,
                    });
                }

                return result;
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to get geo-services", ex);
            }
        }

        public async Task<ExecResult<GeoServiceDto>> UpdateAsync(int serviceId, GeoServiceUpdateDto model)
        {
            try
            {
                var service = await _unitOfWork.GeoServiceRepository.GetAsync(serviceId, true);

                if (service is not null)
                {
                    var result = new ExecResult<GeoServiceDto>();

                    _unitOfWork.BeginTransaction();

                    var cachedStat = _memoryCache.Get<CachedGeoServiceStat>(service.Uri);

                    if (service.Rule is not null)
                    {
                        service.Rule.RequestLimit = model.RequestLimit;
                        if (cachedStat is not null)
                        {
                            cachedStat.RequestLimit = model.RequestLimit;
                            _memoryCache.Set(service.Uri, cachedStat);
                        }
                    }

                    await _unitOfWork.SaveAsync();
                    _unitOfWork.Commit();

                    result.Result = new GeoServiceDto()
                    {
                        Id = service.Id,
                        Name = service.Name!,
                        ServiceUri = service.Uri,
                        RequestLimit = service?.Rule?.RequestLimit ?? 0,
                        RequestCount = cachedStat?.RequestCount ?? 0,
                    };

                    return result;
                }
            }
            catch (Exception ex) when (ex is not RestCoreException)
            {
                throw new Exception("Failed to update geo-service", ex);
            }

            throw new NotFoundCoreException();
        }
    }
}
