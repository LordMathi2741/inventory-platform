namespace ApiGateway.Management.Interface.REST.Resources;

public record CreateProductResource(string Name, string Description, decimal Price, int Stock);