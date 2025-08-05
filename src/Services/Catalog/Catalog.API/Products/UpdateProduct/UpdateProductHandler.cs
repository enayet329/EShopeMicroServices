namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(
	Guid Id,
	string Name,
	string Description,
	decimal Price,
	List<string> Category,
	string ImageFile) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
	: ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
	public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Handling UpdateProductCommand for product with ID: {ProductId}", request.Id);

		var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
		if (product is null)
		{
			throw new ProductNotFoundException();
		}

		product.Name = request.Name;
		product.Description = request.Description;
		product.Price = request.Price;
		product.Category = request.Category;
		product.ImageFile = request.ImageFile;

		session.Store(product);
		await session.SaveChangesAsync(cancellationToken);

		return new UpdateProductResult(true);
	}
}