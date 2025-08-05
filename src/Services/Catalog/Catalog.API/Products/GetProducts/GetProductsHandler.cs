namespace Catalog.API.Products.GetProducts;


public record GetProductsQuery() : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Product);
public class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
	
	public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
	{
		logger.LogInformation("Handling GetProductsQuery");
		var products = await session.Query<Product>().ToListAsync(cancellationToken);
		return new GetProductsResult(products);
	}
}

