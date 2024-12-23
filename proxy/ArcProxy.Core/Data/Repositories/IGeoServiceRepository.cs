using ArcProxy.Core.Data.Entities;

namespace ArcProxy.Core.Data.Repositories
{
    public interface IGeoServiceRepository
    {
        Task<GeoServiceEntity?> GetAsync(int serviceId, bool forEdit = false);
        Task<GeoServiceEntity?> GetAsync(string serviceUri, bool forEdit = false);
        Task<List<GeoServiceEntity>> GetAsync();
    }
}
