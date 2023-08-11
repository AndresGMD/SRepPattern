using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using SRepPattern.Contracts;

namespace SRepPattern;

/// <summary>
/// Repository Pattern for Generic Entities
/// </summary>
/// <typeparam name="TDbContext">DbContext class</typeparam>
/// <typeparam name="TEntity">Entity class</typeparam>

public abstract class SRepPattern<TDbContext, TEntity> where TEntity : class where TDbContext : DbContext
{
    /// <summary>
    /// Factory Repository
    /// </summary>
    /// <param name="dbContext">DbContext Object</param>
    /// <returns></returns>
    public abstract IGenericRepository<TEntity> FactoryRepository(TDbContext dbContext);

    /// <summary>
    /// AddAsync Method
    /// </summary>
    /// <param name="entity">Entity Object</param>
    /// <param name="dbContext">DbContext Object</param>
    /// <returns>Entity added</returns>
    public async Task<TEntity> AddAsync(TEntity entity, TDbContext dbContext)
    {
        var repository = FactoryRepository(dbContext);

        return await repository.AddAsync(entity);
    }

    /// <summary>
    /// AddRangeAsync Method
    /// </summary>
    /// <param name="range">IEnumerable<Entity> Object</param>
    /// <param name="dbContext">DbContext Object</param>
    /// <returns>List of Entities added</returns>
    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> range, TDbContext dbContext)
    {
        var repository = FactoryRepository(dbContext);

        return await repository.AddRangeAsync(range);
    }

    /// <summary>
    /// DeleteAsync Method
    /// </summary>
    /// <param name="entity">Entity object</param>
    /// <param name="dbContext">DbContext Object</param>
    /// <returns>Entity deleted</returns>
    public async Task<TEntity> DeleteAsync(TEntity entity, TDbContext dbContext)
    {
        var repository = FactoryRepository(dbContext);

        return await repository.DeleteAsync(entity);
    }

    /// <summary>
    /// DeleteRangeAsync Method
    /// </summary>
    /// <param name="range">IEnumerable<Entity> Object</param>
    /// <param name="dbContext">DbContext Object</param>
    /// <returns>List of Entities deleted</returns>
    public async Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> range, TDbContext dbContext)
    {
        var repository = FactoryRepository(dbContext);

        return await repository.DeleteRangeAsync(range);
    }

    /// <summary>
    /// GetAllAsync Method
    /// </summary>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <param name="dbContext">DbContext Object</param>
    /// <returns>Get all Items</returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken, TDbContext dbContext)
    {
        var repository = FactoryRepository(dbContext);

        return await repository.GetAllAsync(cancellationToken);
    }

    /// <summary>
    /// GetByIdAsync Method
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <param name="dbContext">DbContext Object</param>
    /// <returns>Get Item by Id</returns>
    public async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken, TDbContext dbContext)
    {
        var repository = FactoryRepository(dbContext);

        return await repository.GetByIdAsync(id, cancellationToken);
    }

    /// <summary>
    /// GetByIdAsync<Tproperty> Method for Relational Entities
    /// </summary>
    /// <typeparam name="TProperty">Property Object</typeparam>
    public async Task<IEnumerable<TProperty>> GetByIdAsync<TProperty>(string propertyId, CancellationToken cancellationToken, TDbContext dbContext, [CallerArgumentExpression("propertyId")] string propertyName = "") where TProperty : class
    {
        var repository = FactoryRepository(dbContext);

        return await repository.GetByIdAsync<TProperty>(propertyId, cancellationToken, propertyName);
    }

    /// <summary>
    /// GetByRangeAsync<Tproperty> Method for Relational Entities
    /// </summary>
    /// <typeparam name="TProperty">Property Object</typeparam>
    public async Task<IEnumerable<TProperty>> GetByRangeAsync<TProperty>(string[] propertyIds, CancellationToken cancellationToken, TDbContext dbContext, [CallerArgumentExpression("propertyIds")] string propertyName = "") where TProperty : class
    {
        var repository = FactoryRepository(dbContext);

        return await repository.GetByRangeAsync<TProperty>(propertyIds, cancellationToken, propertyName);
    }
    
    /// <summary>
    /// UpdateAsync Method
    /// </summary>
    /// <param name="entity">Entity Object</param>
    /// <param name="dbContext">DbContext Object</param>
    /// <returns>Entity Updated</returns>
    public async Task<TEntity> UpdateAsync(TEntity entity, TDbContext dbContext)
    {
        var repository = FactoryRepository(dbContext);

        return await repository.UpdateAsync(entity);
    }

    /// <summary>
    /// UpdateRangeAsync Method
    /// </summary>
    /// <param name="range">IEnumerable<Entity> Object</param>
    /// <param name="dbContext">DbContext Object</param>
    /// <returns>List of Entities Updated</returns>
    public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> range, TDbContext dbContext)
    {
        var repository = FactoryRepository(dbContext);

        return await repository.UpdateRangeAsync(range);
    }
}
