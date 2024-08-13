using CSharpFunctionalExtensions;
using GrpcDiscountGenerator.Domain;

namespace GrpcDiscount.Application.Interfaces;

public interface IDiscountCodeApplier
{
    Task<Result<string>> ApplyCodeAsync(string code);
}
