namespace Basket.API.Data;

public interface IBasketRepository
{
	Task<ShoppingtCart> GetBasket(string UserName, CancellationToken cancellationToken = default);
	Task<ShoppingtCart> StoreBasket(ShoppingtCart shoppingCart, CancellationToken cancellationToken = default);
	Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default);
}
