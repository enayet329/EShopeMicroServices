namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductRequest(
Guid Id,
string Name,
string Description,
decimal Price,
List<string> Category,
string ImageFile);

public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPut("/products",async (UpdateProductRequest request, ISender sender) =>
			{
				var product = request.Adapt<UpdateProductCommand>();
				var result = await sender.Send(product);

				return Results.Ok(new UpdateProductResponse(result.IsSuccess));
			})
			.WithName("UpdateProduct")
			.Produces<UpdateProductResponse>()
			.Produces(StatusCodes.Status400BadRequest)
			.WithDescription("Update Product");
	}
}
