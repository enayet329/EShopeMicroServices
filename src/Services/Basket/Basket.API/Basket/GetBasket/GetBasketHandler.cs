namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingtCart Cart);

public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
	public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
	{
		// TODO: get basket from database
		// var basket = await _basketRepository.GetBasketAsync(request.UserName, cancellationToken);
		return new GetBasketResult(new ShoppingtCart("test"));
	}
}
