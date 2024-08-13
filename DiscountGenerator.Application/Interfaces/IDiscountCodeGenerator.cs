using GrpcDiscountGenerator.Domain;

namespace GrpcDiscount.Application.Interfaces;

public interface IDiscountCodeGenerator
{
    IAsyncEnumerable<Discount> GenerateDiscountCodeAsync(int count, int length);
}
