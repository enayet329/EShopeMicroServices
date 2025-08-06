namespace Basket.API.Basket.StoreBasket;


public record StoreBasketRequest(ShoppingtCart Cart);

public record StoreBasketResponse(string UserName);

public class StoreBasketEndpoint : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapPost("/basket/{userName}", async (StoreBasketRequest request, ISender sender) =>
		{
			var query = request.Adapt<StoreBasketCommand>();
			var result = await sender.Send(query);

			var response = result.Adapt<StoreBasketResponse>();
			return Results.Ok(response);
		})
		.WithName("StoreBasket")
		.Produces<StoreBasketResponse>(StatusCodes.Status201Created)
		.ProducesProblem(StatusCodes.Status400BadRequest)
		.WithSummary("Store Basket")
		.WithDescription("Store Basket");
	}
}
