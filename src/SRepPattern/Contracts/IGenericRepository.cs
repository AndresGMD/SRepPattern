using System.Runtime.CompilerServices;

namespace SRepPattern.Contracts;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> range);
    Task<TEntity> DeleteAsync(TEntity entity);
    Task<IEnumerable<TEntity>> DeleteRangeAsync(IEnumerable<TEntity> range);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken);
    Task<IEnumerable<TProperty>> GetByIdAsync<TProperty>(string propertyId, CancellationToken cancellationToken, [CallerArgumentExpression("propertyId")] string propertyName = "") where TProperty : class;
    Task<IEnumerable<TEntity1>> GetByRangeAsync<TEntity1>(string[] propertyIds, CancellationToken cancellationToken, [CallerArgumentExpression("propertyIds")] string propertyName = "") where TEntity1 : class;
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> range);
}
