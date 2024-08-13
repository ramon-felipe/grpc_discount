using Grpc.Core;
using GrpcDiscount.Application.Interfaces;
using GrpcDiscountGenerator;
using GrpcDiscountGenerator.Domain.ValueObjects;

namespace GrpcDiscount.API.Services;

public class DiscountGeneratorService : DiscountGenerator.DiscountGeneratorBase
{
    private readonly ILogger<DiscountGeneratorService> _logger;
    private readonly IDiscountCodeGenerator _dicountCodeGenerator;

    public DiscountGeneratorService(IDiscountCodeGenerator dicountCodeGenerator, ILogger<DiscountGeneratorService> logger)
    {
        this._dicountCodeGenerator = dicountCodeGenerator;
        this._logger = logger;
    }

    public override async Task<DiscountGeneratorReply> Generate(DiscountGeneratorRequest request, ServerCallContext context)
    {
        if (!DiscountCode.HasValidLength(request.Length))
            throw new ArgumentException("Invalid Discount Length");

        await this._dicountCodeGenerator.GenerateCodesAsync(request.Count, request.Length);

        return new DiscountGeneratorReply { Result = true };
    }
}
