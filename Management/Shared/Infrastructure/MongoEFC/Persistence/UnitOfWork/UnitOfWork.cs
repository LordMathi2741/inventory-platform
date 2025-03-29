using Management.Shared.Domain.Repositories;

namespace Management.Shared.Infrastructure.MongoEFC.Persistence.UnitOfWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}