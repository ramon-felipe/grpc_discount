using GrpcDiscount.Application.Interfaces;
using GrpcDiscountGenerator.Domain;
using GrpcDiscountGenerator.Infrastructure.Repositories;

namespace GrpcDiscount.Application;

public sealed class DiscountCodeGenerator : IDiscountCodeGenerator
{
    private readonly IDiscountHelper _discountHelper;
    private readonly IRepository<Discount> _repository;

    public DiscountCodeGenerator(IDiscountHelper discountHelper, IRepository<Discount> repository)
    {
        this._discountHelper = discountHelper;
        this._repository = repository;
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
            var uniqueCode = this.GenerateUniqueDiscountCode(length);
            var discount = new Discount(uniqueCode);

            this._repository.Add(discount);
            this._repository.Save();

            return discount;
        });
    }

    private string GenerateUniqueDiscountCode(int length)
    {
        var generatedCode = string.Empty;
        var done = false;

        do
        {
            generatedCode = this._discountHelper.GenerateDiscount(length);
            var alreadyExists = this._repository.Get(_ => _.Code.Value == generatedCode);

            if (alreadyExists.HasNoValue)
                done = true;

        } while (!done);

        return generatedCode;
    }
}
