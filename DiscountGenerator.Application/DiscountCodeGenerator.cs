using GrpcDiscountGenerator.Domain.ValueObjects;
using GrpcDiscountGenerator.Domain;

namespace GrpcDiscount.Application;

public sealed class DiscountCodeGenerator : IDiscountCodeGenerator
{
    private readonly IDiscountHelper _discountHelper;

    public DiscountCodeGenerator(IDiscountHelper discountHelper)
    {
        this._discountHelper = discountHelper;
    }

    public async IAsyncEnumerable<Discount> GenerateDiscountCodeAsync(int count, int length)
    {
        do
        {
            count--;
            yield return await this.GenerateCodeAsync(length);

        } while (count > 0);
    }

    private Task<Discount> GenerateCodeAsync(int length)
    {
        return Task.Run(() =>
        {
            return new Discount(this._discountHelper.GenerateDiscount(length));
        });
    }
}
