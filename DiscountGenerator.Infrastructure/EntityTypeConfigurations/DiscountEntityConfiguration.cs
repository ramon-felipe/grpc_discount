using GrpcDiscountGenerator.Domain;
using GrpcDiscountGenerator.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GrpcDiscountGenerator.Infrastructure.EntityTypeConfigurations;

internal class DiscountEntityConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("Discounts");

        builder.HasKey(_ => _.Id);

        builder
            .Property(_ => _.Code)
            .HasConversion(_ => _.Value, _ => DiscountCode.Create(_).Value)
            .HasColumnName("DiscountCode");
    }
}