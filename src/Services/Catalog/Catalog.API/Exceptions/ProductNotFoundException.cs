namespace Catalog.API.Exceptions;

public class ProductNotFoundException : ApplicationException
{
	public ProductNotFoundException(string message) : base(message)
	{
	}

	public ProductNotFoundException() : this("Product not found.")
	{
	}
	public ProductNotFoundException(string message, System.Exception innerException)
		: base(message, innerException)
	{
	}
}