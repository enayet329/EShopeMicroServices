
namespace Catalog.API.Exceptions;

public class ProductNotFoundException : NotFoundException
{

	public ProductNotFoundException(Guid id)
		: base($"Product with id {id} was not found.")
	{
	}
}