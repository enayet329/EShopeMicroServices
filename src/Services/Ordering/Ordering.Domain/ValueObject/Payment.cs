namespace Ordering.Domain.ValueObject;

public record Payment
{
	public string CardName { get; } = default!;
	public string CardNumber { get; } = default!;
	public DateTime Expiration { get; } = default;
	public string CVV { get; } = default!;
	public int PaymentMethod { get; } = default;
}
