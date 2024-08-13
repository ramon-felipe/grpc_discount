using Grpc.Core;
using GrpcDiscountApplier;

namespace GrpcDiscount.API.Services;

public sealed class DiscountApplierService : DiscountApplier.DiscountApplierBase
{
    public override Task<UseCodeReply> Apply(UseCodeRequest request, ServerCallContext context)
    {
        return base.Apply(request, context);
    }
}