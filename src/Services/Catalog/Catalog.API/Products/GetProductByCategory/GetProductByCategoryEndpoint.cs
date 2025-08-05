namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
		{
			var products = await sender.Send(new GetProductByCategoryQuery(category));

			var result = products.Adapt<GetProductByCategoryResult>();
			return Results.Ok(result);

		})
		.WithName("GetProductsByCategory")
		.Produces<GetProductByCategoryResult>(StatusCodes.Status201Created)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Get Products By Category")
		.WithDescription("Get Products By Category");
	}
}
