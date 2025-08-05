namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResponse(Product Product);
public class GetProductByIdEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
		{
			var product = await sender.Send(new GetProductByIdQuery(id));
			if (product is null)
			{
				return Results.NotFound();
			}

			var result = product.Adapt< GetProductByIdResponse>();

			return Results.Ok(result);
		})
		.WithName("GetProductsById")
		.Produces<GetProductByIdResponse>(StatusCodes.Status201Created)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Get Products By Id")
		.WithDescription("Get Products By Id");
	}
}
