using MongoDB.Bson;

namespace Management.Shared.Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<bool> AddAsync(TEntity entity);
    Task<bool> UpdateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(ObjectId id);
    Task<bool> DeleteAsync(ObjectId id);
}