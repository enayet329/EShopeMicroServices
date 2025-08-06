namespace Basket.API.Models;

public class ShoppingtCart
{
	public string UserName { get; set; } = default!;
	public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
	public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
	public ShoppingtCart(string userName)
	{
		UserName = userName;
	}

	public ShoppingtCart()
	{
	}
}