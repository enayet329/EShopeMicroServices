namespace Catalog.API.Products.GetProductByCategory;


public record GetProductByCategoryQuery(string category) : IQuery<GetProductByCategoryResponse>;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResponse>
{

	public async Task<GetProductByCategoryResponse> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
	{
		logger.LogInformation("Handling GetProductByCategoryQuery for category {Category}", query.category);

		var products = await session.Query<Product>()
			.Where(p => p.Category.Contains(query.category))
			.ToListAsync(cancellationToken);

		logger.LogInformation("Retrieved {Count} products for category {Category}", products.Count, query.category);

		return new GetProductByCategoryResponse(products);
	}
}
