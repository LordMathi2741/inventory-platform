using Management.Domain.Repositories;
using Management.Domain.Service;
using Support.Management.Domain.Model.Aggregates;
using Support.Management.Domain.Model.Queries;

namespace Management.Application.Internal.QueryServices;

public class ProductQueryService(IProductRepository productRepository) : IProductQueryService
{
    public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery query)
    {
        return await productRepository.GetAllAsync();
    }

    public async Task<Product?> Handle(GetProductByIdQuery query)
    {
        return await productRepository.GetByIdAsync(query.Id);
    }
}