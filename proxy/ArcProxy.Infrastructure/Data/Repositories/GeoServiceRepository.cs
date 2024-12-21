using ArcProxy.Core.Data.Entities;
using ArcProxy.Core.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArcProxy.Infrastructure.Data.Repositories
{
    internal class GeoServiceRepository : IGeoServiceRepository
    {
        private DatabaseContext m_DbContext;
        public GeoServiceRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public async Task<GeoServiceEntity?> GetAsync(int serviceId)
        {
            return await m_DbContext.GeoServices.AsNoTracking()
                .Include(s => s.Rule)
                .SingleOrDefaultAsync(s => s.Id == serviceId);
        }

        public async Task<GeoServiceEntity?> GetAsync(string serviceUri)
        {
            return await m_DbContext.GeoServices.AsNoTracking()
                .Include(s => s.Rule)
                .SingleOrDefaultAsync(s => s.Uri == serviceUri);
        }
    }
}
