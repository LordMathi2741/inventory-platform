using Shared.Domain.Repositories;

namespace Shared.Infrastructure.MongoEFC.Persistence.UnitOfWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}