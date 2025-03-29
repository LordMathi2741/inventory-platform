namespace Management.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}