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

        public Task<GeoServiceEntity?> GetAsync(int serviceId, bool forEdit = false)
        {
            IQueryable<GeoServiceEntity> result = m_DbContext.GeoServices;
            if (!forEdit)
            {
                result = result.AsNoTracking();
            }
            return result.Include(s => s.Rule)
                .SingleOrDefaultAsync(s => s.Id == serviceId);
        }

        public Task<GeoServiceEntity?> GetAsync(string serviceUri, bool forEdit = false)
        {
            IQueryable<GeoServiceEntity> result = m_DbContext.GeoServices;
            if (!forEdit)
            {
                result = result.AsNoTracking();
            }
            return result.Include(s => s.Rule)
                .SingleOrDefaultAsync(s => s.Uri == serviceUri);
        }

        public Task<List<GeoServiceEntity>> GetAsync()
        {
            return m_DbContext.GeoServices.AsNoTracking()
                .Include(g => g.Rule)
                .ToListAsync();
        }
    }
}
