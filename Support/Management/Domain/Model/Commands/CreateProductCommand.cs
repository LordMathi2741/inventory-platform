namespace Support.Management.Domain.Model.Commands;
public record CreateProductCommand(string Name, string Description, decimal Price, int Stock);
