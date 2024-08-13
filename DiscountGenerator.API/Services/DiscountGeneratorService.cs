using Grpc.Core;
using GrpcDiscount.Application.Interfaces;
using GrpcDiscountGenerator;

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
        if (request.Length < 7 || request.Length > 8)
            throw new ArgumentException("Invalid Discount Length");

        await this._dicountCodeGenerator.GenerateCodesAsync(request.Count, request.Length);

        return new DiscountGeneratorReply { Result = true };
    }
}
