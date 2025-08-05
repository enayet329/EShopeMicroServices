namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger)
	: ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
	public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Handling DeleteProductCommand for product with ID: {ProductId}", request.ProductId);

		session.Delete<Product>(request.ProductId);

		await session.SaveChangesAsync(cancellationToken);

		return new DeleteProductResult(true);
	}
}