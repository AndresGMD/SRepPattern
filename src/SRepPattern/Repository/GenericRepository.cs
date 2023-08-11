using System.Runtime.CompilerServices;
using SRepPattern.Contracts;
using Microsoft.EntityFrameworkCore;
using SRepPattern.Extensions;

namespace SRepPattern.Repository;


public class GenericRepository<TDbContext, TEntity> : IGenericRepository<TEntity> where TEntity : class where TDbContext : DbContext
{
    private TDbContext _context;
    protected DbSet<TEntity> _table;

    public GenericRepository(TDbContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        if(string.IsNullOrWhiteSpace(entity.GetPropertyValue("Id")))
            entity.SetPropertyValue("Id", Guid.NewGuid().ToString());
        await _table.AddAsync(entity);
        await SaveAsync();
        return entity;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> range)
    {
        range.ToList().ForEach(item => {
            if(string.IsNullOrWhiteSpace(item.GetPropertyValue("Id")))
                item.SetPropertyValue("Id", Guid.NewGuid().ToString());
        });
        await _table.AddRangeAsync(range);
        await SaveAsync();
        return range;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        _table.Remove(entity);
        await SaveAsync();
        return entity;
    }

    public async Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> range)
    {
        _table.RemoveRange(range);
        await SaveAsync();
        return range;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _table.ToListAsync(cancellationToken);
    }


    public async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken)
    {
        return await _table.FindAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<TProperty>> GetByIdAsync<TProperty>(string propertyId, CancellationToken cancellationToken, [CallerArgumentExpression(nameof(propertyId))] string propertyName = "") where TProperty : class
    {
        var result = await _table.AsNoTracking()
                     .WhereByProperty(propertyId, propertyName)
                     .SelectEntity<TEntity, TProperty>()
                     .ToListAsync();

        return (IEnumerable<TProperty>)(result);

    }

    public async Task<IEnumerable<TProperty>> GetByRangeAsync<TProperty>(string[] propertyIds, CancellationToken cancellationToken, [CallerArgumentExpression(nameof(propertyIds))] string propertyName = "") where TProperty : class
    {
        propertyName = propertyName.Substring(0, propertyName.Length - 1);
        var result = await (_table!.AsNoTracking()
                            .IncludeEntity<TEntity,TProperty>()
                            .WhereContains(propertyIds,propertyName)
                            .SelectEntity<TEntity,TProperty>()
                            .Distinct()
                            .ToListAsync(cancellationToken));
        return (IEnumerable<TProperty>)(result);

    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _table.Update(entity);
        await SaveAsync();
        return entity;
    }

    public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> range)
    {
        _table.UpdateRange(range);
        await SaveAsync();
        return range;
    }

    private async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
