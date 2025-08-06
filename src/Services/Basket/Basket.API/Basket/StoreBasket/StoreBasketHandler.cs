namespace Basket.API.Basket.StoreBaskt;

public record StoreBasketCommand(ShoppingtCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
	public StoreBasketCommandValidator()
	{
		RuleFor(x => x.Cart).NotNull().WithMessage("Shopping cart cannot be null.");
		RuleFor(x => x.Cart.UserName)
			.NotEmpty()
			.WithMessage("User name cannot be empty.")
			.MaximumLength(100)
			.WithMessage("User name cannot exceed 100 characters.");
	}
}

public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
	public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
	{
		// TODO: Implement the logic to store the shopping cart.	
		// This could involve saving the cart to a database or an external service.
		// and Update cash
		return new StoreBasketResult("true");
	}
}
