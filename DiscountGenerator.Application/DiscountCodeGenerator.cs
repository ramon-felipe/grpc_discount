using GrpcDiscount.Application.Interfaces;
using GrpcDiscountGenerator.Domain;
using GrpcDiscountGenerator.Infrastructure.Repositories;
using System.Threading.Tasks;

namespace GrpcDiscount.Application;

public sealed class DiscountCodeGenerator : IDiscountCodeGenerator
{
    private readonly IDiscountHelper _discountHelper;
    private readonly IRepository<Discount> _repository;
    private readonly object _lock = new ();

    public DiscountCodeGenerator(IDiscountHelper discountHelper, IRepository<Discount> repository)
    {
        this._discountHelper = discountHelper;
        this._repository = repository;
    }

    public Task<HashSet<Discount>> GenerateCodesAsync(int count, int length)
    {
        return Task.Run(() =>
        {
            var codes = new HashSet<Discount>();

            lock (_lock)
            {
                do
                {
                    count--;
                    codes.Add(this.GenerateCode(length));

                } while (count > 0);

                this._repository.BulkInsert(codes);
            }

            return codes;
        });
    }

    private Discount GenerateCode(int length)
    {
        var uniqueCode = this.GenerateUniqueDiscountCode(length);
        var discount = new Discount(uniqueCode);

        return discount;
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
