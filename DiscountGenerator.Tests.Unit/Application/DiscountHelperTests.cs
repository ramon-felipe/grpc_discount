using GrpcDiscount.Application;
using FluentAssertions;
using GrpcDiscountGenerator.Domain;
using GrpcDiscountGenerator.Infrastructure.Repositories;
using NSubstitute;

namespace DiscountGenerator.Tests.Unit.Application;

public class DiscountHelperTests
{
    private readonly DiscountHelper discountHelper;
    private readonly IRepository<Discount> _repository;

    public DiscountHelperTests()
    {
        this._repository = Substitute.For<IRepository<Discount>>();
        discountHelper = new DiscountHelper(this._repository);
    }

    [Theory]
    [InlineData(7)]
    [InlineData(8)]
    public void Should_GenerateDiscountCode_Successfully(int length)
    {
        // Act
        var result = discountHelper.GenerateDiscount(length);

        // Assert
        result.Should().HaveLength(length);
    }
}
