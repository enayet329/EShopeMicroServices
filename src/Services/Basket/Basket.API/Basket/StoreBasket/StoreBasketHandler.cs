using Discount.Grpc;

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

public class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
	public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
	{
		await DeductDiscount(command.Cart, cancellationToken);

		await repository.StoreBasket(command.Cart, cancellationToken);
		
		return new StoreBasketResult(command.Cart.UserName);
	}

	private async Task DeductDiscount(ShoppingtCart cart, CancellationToken cancellationToken)
	{
		foreach (var item in cart.Items)
		{
			var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);

			item.Price -= coupon.Amount;
		}
	}

}
