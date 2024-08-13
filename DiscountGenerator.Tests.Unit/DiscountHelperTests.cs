using GrpcDiscount.Application;
using FluentAssertions;

namespace GrpcDiscountGenerator.Tests.Unit;

public class DiscountHelperTests
{
    private readonly DiscountHelper discountHelper;

    public DiscountHelperTests()
    {
        discountHelper = new DiscountHelper();
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
