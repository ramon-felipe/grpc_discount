using CSharpFunctionalExtensions;
using GrpcDiscountGenerator.Domain;

namespace GrpcDiscount.Application.Interfaces;

public interface IDiscountCodeGenerator
{
    Task<HashSet<Discount>> GenerateCodesAsync(int count, int length);
}
