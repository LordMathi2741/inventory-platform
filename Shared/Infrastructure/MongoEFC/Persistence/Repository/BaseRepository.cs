using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Shared.Domain.Repositories;

namespace Shared.Infrastructure.MongoEFC.Persistence.Repository;

public class BaseRepository<TEntity>(AppDbContext context) : IBaseRepository<TEntity> where TEntity : class
{
    public async Task<bool> AddAsync(TEntity entity)
    {
        await context.AddAsync(entity);
        return true;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        context.Update(entity);
        return true;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(ObjectId id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public async Task<bool> DeleteAsync(ObjectId id)
    {
        var entity = await GetByIdAsync(id);
        context.Remove(entity);
        return true;
    }
}