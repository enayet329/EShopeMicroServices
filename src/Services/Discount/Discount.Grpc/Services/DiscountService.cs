using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService
	(DiscountContext dbContext, ILogger<DiscountService> logger)
	: DiscountProtoService.DiscountProtoServiceBase
{
	public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
	{
		var coupon = await dbContext
			.Coupons
			.FirstOrDefaultAsync(c => c.ProductName == request.ProductName, context.CancellationToken);

		if (coupon == null)
			coupon = new Coupon
			{
				ProductName = request.ProductName,
				Description = "No Discount",
				Amount = 0
			};

		logger.LogInformation("Discount retrieved for product {ProductName}", request.ProductName);

		var couponModel = coupon.Adapt<CouponModel>();
		return couponModel;
	}

	public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
	{
		var coupon = request.Coupon.Adapt<Coupon>();

		if (coupon == null)
		{
			throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon cannot be null."));
		}

		dbContext.Coupons.Add(coupon);
		await dbContext.SaveChangesAsync(context.CancellationToken);

		logger.LogInformation("Discount created for product {ProductName}", coupon.ProductName);

		return coupon.Adapt<CouponModel>();
	}

	public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
	{
		var coupon = await dbContext.Coupons
			.FirstOrDefaultAsync(c => c.ProductName == request.Coupon.ProductName, context.CancellationToken);

		if (coupon == null)
		{
			throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.Coupon.ProductName} not found."));
		}

		coupon.Description = request.Coupon.Description;
		coupon.Amount = (decimal)request.Coupon.Amount;

		await dbContext.SaveChangesAsync(context.CancellationToken);

		logger.LogInformation("Discount updated for product {ProductName}", coupon.ProductName);

		return coupon.Adapt<CouponModel>();
	}

	public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
	{
		var coupon = await dbContext.Coupons
			.FirstOrDefaultAsync(c => c.ProductName == request.ProductName, context.CancellationToken);

		if (coupon == null)
		{
			return new DeleteDiscountResponse { Success = false };
		}

		dbContext.Coupons.Remove(coupon);
		await dbContext.SaveChangesAsync(context.CancellationToken);

		logger.LogInformation("Discount deleted for product {ProductName}", request.ProductName);

		return new DeleteDiscountResponse { Success = true };
	}
}
