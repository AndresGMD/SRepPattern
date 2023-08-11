using Microsoft.EntityFrameworkCore;
using SRepPattern.Contracts;
using SRepPattern.Repository;

namespace SRepPattern;

public class SRepPatternConcrete<TDbContext, TEntity> : SRepPattern<TDbContext, TEntity> where TEntity : class where TDbContext : DbContext
{
    public override IGenericRepository<TEntity> FactoryRepository(TDbContext dbContext)
    {
        return new GenericRepository<TDbContext, TEntity>(dbContext);
    }
}
