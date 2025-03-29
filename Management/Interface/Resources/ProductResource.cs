namespace ApiGateway.Management.Interface.REST.Resources;

public record ProductResource(string Id, string Name, string Description, decimal Price, int Stock);