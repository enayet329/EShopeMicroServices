namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);


public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
	public DeleteProductCommandValidator()
	{
		RuleFor(x => x.ProductId)
			.NotEmpty().WithMessage("Product ID must not be empty.");
	}
}

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