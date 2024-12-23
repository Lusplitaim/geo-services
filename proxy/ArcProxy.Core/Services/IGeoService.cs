using ArcProxy.Core.Models;
using ArcProxy.Core.Models.DTOs;

namespace ArcProxy.Core.Services
{
    public interface IGeoService
    {
        Task<bool> TryAccessAsync(string servicePath);
        Task<IEnumerable<GeoServiceDto>> GetAsync();
        Task<ExecResult<GeoServiceDto>> UpdateAsync(int serviceId, GeoServiceUpdateDto model);
    }
}