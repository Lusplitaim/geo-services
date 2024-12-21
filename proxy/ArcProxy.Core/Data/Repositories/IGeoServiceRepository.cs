using ArcProxy.Core.Data.Entities;

namespace ArcProxy.Core.Data.Repositories
{
    public interface IGeoServiceRepository
    {
        Task<GeoServiceEntity?> GetAsync(int serviceId);
        Task<GeoServiceEntity?> GetAsync(string serviceUri);
    }
}
