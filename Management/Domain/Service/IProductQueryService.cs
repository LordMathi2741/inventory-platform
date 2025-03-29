using Management.Domain.Model.Aggregates;
using Management.Domain.Model.Queries;

namespace Management.Domain.Service;

public interface IProductQueryService
{
    Task<IEnumerable<Product>> Handle(GetAllProductsQuery query);
    Task<Product?> Handle(GetProductByIdQuery query);
}