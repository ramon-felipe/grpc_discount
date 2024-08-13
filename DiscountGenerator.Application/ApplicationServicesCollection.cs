using GrpcDiscount.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GrpcDiscount.Application;

public static class ApplicationServicesCollection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IDiscountHelper, DiscountHelper>()
            .AddScoped<IDiscountCodeGenerator, DiscountCodeGenerator>()
            .AddScoped<IDiscountCodeApplier, DiscountCodeApplier>()
            ;
    }
}