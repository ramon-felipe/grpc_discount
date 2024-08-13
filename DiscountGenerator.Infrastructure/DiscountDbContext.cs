using GrpcDiscountGenerator.Domain;
using GrpcDiscountGenerator.Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace GrpcDiscountGenerator.Infrastructure;

public class DiscountDbContext : DbContext
{
    public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
    {
    }

    public DbSet<Discount> Discounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DiscountEntityConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
