using ArcProxy.Core.Data.Repositories;

namespace ArcProxy.Infrastructure.Data.Repositories
{
    internal class GeoServiceRuleRepository : IGeoServiceRuleRepository
    {
        private DatabaseContext m_DbContext;
        public GeoServiceRuleRepository(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }
    }
}
