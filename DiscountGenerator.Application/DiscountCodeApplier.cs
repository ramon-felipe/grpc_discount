using CSharpFunctionalExtensions;
using GrpcDiscount.Application.Interfaces;
using GrpcDiscountGenerator.Domain;
using GrpcDiscountGenerator.Infrastructure.Repositories;

namespace GrpcDiscount.Application;

public sealed class DiscountCodeApplier : IDiscountCodeApplier
{
    private readonly IRepository<Discount> _repository;
    private readonly object _lock = new();

    public DiscountCodeApplier(IRepository<Discount> repository)
    {
        this._repository = repository;
    }

    public Task<Result<string>> ApplyCodeAsync(string code)
    {
        return Task.Run(() =>
        {
            lock (_lock)
            {
                var discount = this._repository.Get(_ => _.Code.Value == code);

                if (discount.HasNoValue)
                    return Result.Failure<string>("No discount found");

                this._repository.Delete(discount.Value);
                this._repository.Save();
            }

            return code;
        });
    }
}
