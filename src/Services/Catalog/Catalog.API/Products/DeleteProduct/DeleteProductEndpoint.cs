namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/products/{productId:guid}", async (Guid productId, ISender sender) =>
		{
			var result = await sender.Send(new DeleteProductCommand(productId));

			return Results.Ok(new DeleteProductResponse(result.IsSuccess));
		})
		.WithName("DeleteProductsById")
		.Produces<DeleteProductResponse>(StatusCodes.Status200OK)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Delete Product By Id")
		.WithDescription("Delete Product By Id");
	}
}