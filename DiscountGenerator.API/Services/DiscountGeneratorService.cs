using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcDiscount.Application.Interfaces;
using GrpcDiscountGenerator;
using GrpcDiscountGenerator.Domain.ValueObjects;

namespace GrpcDiscount.API.Services;

public class DiscountGeneratorService : DiscountGenerator.DiscountGeneratorBase
{
    private readonly ILogger<DiscountGeneratorService> _logger;
    private readonly IDiscountCodeGenerator _dicountCodeGenerator;
    private readonly IDiscountHelper _discountHelper;

    public DiscountGeneratorService(IDiscountCodeGenerator dicountCodeGenerator, ILogger<DiscountGeneratorService> logger, IDiscountHelper discountHelper)
    {
        this._dicountCodeGenerator = dicountCodeGenerator;
        this._logger = logger;
        this._discountHelper = discountHelper;
    }

    public override async Task<DiscountGeneratorReply> Generate(DiscountGeneratorRequest request, ServerCallContext context)
    {
        if (!DiscountCode.HasValidLength(request.Length))
            throw new ArgumentException("Invalid Discount Length");

        await this._dicountCodeGenerator.GenerateCodesAsync(request.Count, request.Length);

        return new DiscountGeneratorReply { Result = true };
    }

    public override Task<DiscountGeneratorGetLastReply> GetLast(Empty request, ServerCallContext context)
    {
        var maybeLast = this._discountHelper.GetLast();

        var res = maybeLast.HasValue ? new DiscountGeneratorGetLastReply { Code = maybeLast.Value.Code.Value } : new DiscountGeneratorGetLastReply();

        return Task.FromResult(res);
    }
}
