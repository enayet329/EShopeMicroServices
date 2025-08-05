namespace Catalog.API.Products.CreateProduct;


public record CreateProductCommand(
	string Name,
	string Description,
	decimal Price,
	List<string> Category,
	string ImageFile
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);


public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
	public CreateProductCommandValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.WithMessage("Name is required");

		RuleFor(x => x.Description)
			.NotEmpty()
			.WithMessage("Description is required");

		RuleFor(x => x.Price)
			.GreaterThan(0)
			.WithMessage("Price must be greater than zero");

		RuleFor(x => x.Category)
			.NotEmpty()
			.WithMessage("Category is required");

		RuleFor(x => x.ImageFile)
			.NotEmpty()
			.WithMessage("ImageFile is required");
	}
}

internal class CreateProductHandler(IDocumentSession session, IValidator<CreateProductCommand> validator) 
	: ICommandHandler<CreateProductCommand, CreateProductResult>
{
	public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
	{
		var validateResult = await validator.ValidateAsync(command, cancellationToken);
		var erro = validateResult.Errors.Select(x => x.ErrorMessage).ToList();
		if(erro.Any())
		{
			throw new ValidationException(erro.FirstOrDefault());
		}

		var product = new Product
		{
			Name = command.Name,
			Description = command.Description,
			Price = command.Price,
			Category = command.Category,
			ImageFile = command.ImageFile
		};


		session.Store(product);
		await session.SaveChangesAsync(cancellationToken);

		return new CreateProductResult(product.Id);
	}
}
