using ArcProxy.Core.Data;
using ArcProxy.Core.Data.Repositories;
using ArcProxy.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ArcProxy.Infrastructure.Data
{
    internal class UnitOfWork : IUnitOfWork
    {
        private DatabaseContext m_DbContext;
        public UnitOfWork(DatabaseContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public IGeoServiceRepository GeoServiceRepository => new GeoServiceRepository(m_DbContext);
        public IGeoServiceRuleRepository GeoServiceRuleRepository => new GeoServiceRuleRepository(m_DbContext);

        public async Task SaveAsync()
        {
            await m_DbContext.SaveChangesAsync();
        }

        public void Commit()
        {
            m_DbContext.Database.CommitTransaction();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return m_DbContext.Database.BeginTransaction();
        }
    }
}
