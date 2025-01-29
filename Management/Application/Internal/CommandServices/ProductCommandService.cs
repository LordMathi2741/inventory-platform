using Management.Domain.Repositories;
using Management.Domain.Service;
using Shared.Domain.Repositories;
using Support.Management.Domain.Model.Aggregates;
using Support.Management.Domain.Model.Commands;

namespace Management.Application.Internal.CommandServices;

public class ProductCommandService(IUnitOfWork unitOfWork, IProductRepository productRepository) : IProductCommandService
{
    public async Task Handle(CreateProductCommand command)
    {
        var product = new Product(command);
        await productRepository.AddAsync(product);
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(UpdateProductCommand command)
    {
        var product = await productRepository.GetByIdAsync(command.Id);
        if (product == null)
        {
            throw new Exception("Product not found");
        }
        product.Update(command);
        await unitOfWork.CompleteAsync();
    }

    public async Task Handle(DeleteProductCommand command)
    {
        await productRepository.DeleteAsync(command.Id);
        await unitOfWork.CompleteAsync();
    }
}