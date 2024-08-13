using GrpcDiscount.Application;
using FluentAssertions;

namespace DiscountGenerator.Tests.Unit.Application;

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
