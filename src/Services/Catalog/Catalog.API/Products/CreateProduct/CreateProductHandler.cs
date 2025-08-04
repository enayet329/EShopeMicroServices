using BuildingBlocks.CQRS;
using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Products.CreateProduct;


public record CreateProductCommand(
	string Name,
	string Description,
	decimal Price,
	List<string> Category,
	string ImageFile
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductHandler 
	: ICommandHandler<CreateProductCommand, CreateProductResult>
{
	public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
	{
		// create product entity 

		var product = new Product
		{
			Name = command.Name,
			Description = command.Description,
			Price = command.Price,
			Category = command.Category,
			ImageFile = command.ImageFile
		};


		// TODO :  save it to the database
		// return the result with the product ID

		return new CreateProductResult(Guid.NewGuid());
	}
}
