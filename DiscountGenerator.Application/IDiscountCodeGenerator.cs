using GrpcDiscountGenerator.Domain;

namespace GrpcDiscount.Application;

public interface IDiscountCodeGenerator
{
    IAsyncEnumerable<Discount> GenerateDiscountCodeAsync(int count, int length);
}
