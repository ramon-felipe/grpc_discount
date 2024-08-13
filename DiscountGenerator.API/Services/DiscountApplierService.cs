using Grpc.Core;
using GrpcDiscount.Application.Interfaces;
using GrpcDiscountApplier;

namespace GrpcDiscount.API.Services;

public sealed class DiscountApplierService : DiscountApplier.DiscountApplierBase
{
    private readonly IDiscountCodeApplier _discountCodeApplier;

    public DiscountApplierService(IDiscountCodeApplier discountCodeApplier)
    {
        this._discountCodeApplier = discountCodeApplier;
    }

    public override async Task<UseCodeReply> Apply(UseCodeRequest request, ServerCallContext context)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(request.Code);

        var result = await this._discountCodeApplier.ApplyCodeAsync(request.Code);

        return result.IsFailure
            ? new UseCodeReply { Result = result.Error }
            : new UseCodeReply { Result = $"code [{request.Code}] applied successfully" };
    }
}