using Management.Domain.Model.Aggregates;
using Management.Domain.Repositories;
using Management.Shared.Infrastructure.MongoEFC.Persistence;
using Management.Shared.Infrastructure.MongoEFC.Persistence.Repository;

namespace Management.Infrastructure.Persistence.MongoEFC.Repositories;

public class ProductRepository(AppDbContext context) :  BaseRepository<Product>(context), IProductRepository
{
   
}