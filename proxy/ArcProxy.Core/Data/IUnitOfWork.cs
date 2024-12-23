using ArcProxy.Core.Data.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace ArcProxy.Core.Data
{
    public interface IUnitOfWork
    {
        IGeoServiceRepository GeoServiceRepository { get; }
        IGeoServiceRuleRepository GeoServiceRuleRepository { get; }
        Task SaveAsync();
        void Commit();
        IDbContextTransaction BeginTransaction();
    }
}
