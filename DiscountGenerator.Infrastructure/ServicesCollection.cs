using GrpcDiscountGenerator.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GrpcDiscountGenerator.Infrastructure;

public static class ServicesCollection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connString = config.GetSection("ConnectionStrings:SqlServer").Value;

        return services
            .AddDbContext<DiscountDbContext>(options =>
            {
                options.UseSqlServer(connString);
            })
            .AddScoped(typeof(IRepository<>), typeof(GenericRepository<>))
        ;
    }
}