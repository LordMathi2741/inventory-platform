using Management.Domain.Repositories;
using Shared.Infrastructure.MongoEFC.Persistence;
using Shared.Infrastructure.MongoEFC.Persistence.Repository;
using Support.Management.Domain.Model.Aggregates;

namespace Management.Infrastructure.Persistence.MongoEFC.Repositories;

public class ProductRepository(AppDbContext context) :  BaseRepository<Product>(context), IProductRepository
{
   
}