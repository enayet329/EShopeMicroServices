
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data;

public class CachedBasketReository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
{

	public async Task<ShoppingtCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
	{
		var chachedBasket = await cache.GetStringAsync(UserName, cancellationToken);
		if (!string.IsNullOrEmpty(chachedBasket))
			return JsonSerializer.Deserialize<ShoppingtCart>(chachedBasket)!;

		var basket = await repository.GetBasket(UserName, cancellationToken);

		await cache.SetStringAsync(UserName, JsonSerializer.Serialize(basket), cancellationToken);

		return basket;
	}

	public async Task<ShoppingtCart> StoreBasket(ShoppingtCart shoppingCart, CancellationToken cancellationToken = default)
	{
	    await repository.StoreBasket(shoppingCart, cancellationToken);

		await cache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart), cancellationToken);

		return shoppingCart;

	}
	public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default)
	{
		await repository.DeleteBasket(UserName, cancellationToken);

		await cache.RemoveAsync(UserName, cancellationToken);

		return true;
	}

}
