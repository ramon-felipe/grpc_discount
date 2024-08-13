using GrpcDiscountGenerator.Domain.ValueObjects;

namespace GrpcDiscountGenerator.Domain;

public sealed class Discount : BaseEntity
{
    public Discount(string code)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);

        var discountCodeResult = DiscountCode.Create(code);

        if (discountCodeResult.IsFailure)
            throw new ArgumentException(discountCodeResult.Error);

        this.Code = discountCodeResult.Value;
    }

    public DiscountCode Code { get; private set; } = DiscountCode.CreateEmpty();
}
