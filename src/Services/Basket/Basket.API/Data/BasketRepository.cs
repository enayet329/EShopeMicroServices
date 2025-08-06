namespace Basket.API.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
	public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
	{
		session.Delete<ShoppingtCart>(userName);
		await session.SaveChangesAsync(cancellationToken);
		return true; 
	}

	public async Task<ShoppingtCart> GetBasket(string userName, CancellationToken cancellationToken = default)
	{
		var basket = await session.LoadAsync<ShoppingtCart>(userName, cancellationToken);

		return basket is null ? throw new BasketNotFoundException(userName) : basket;
	}

	public async Task<ShoppingtCart> StoreBasket(ShoppingtCart shoppingCart, CancellationToken cancellationToken = default)
	{
		session.Store(shoppingCart);
		await session.SaveChangesAsync(cancellationToken); 
		return shoppingCart;
	}
}