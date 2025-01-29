using Support.Management.Domain.Model.Commands;

namespace Management.Domain.Service;

public interface IProductCommandService
{
    Task Handle(CreateProductCommand command);
    Task Handle(UpdateProductCommand command);
    Task Handle(DeleteProductCommand command);
}