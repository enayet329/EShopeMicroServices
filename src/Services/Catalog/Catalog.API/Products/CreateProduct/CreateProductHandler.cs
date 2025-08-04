using MediatR;

namespace Catalog.API.Products.CreateProduct;


public record CreateProductCommand(
	string Name,
	string Description,
	decimal Price,
	string Category,
	string ImageUrl
) : IRequest<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
	public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
	{
		// business logic to create product
		throw new NotImplementedException();
	}
}
