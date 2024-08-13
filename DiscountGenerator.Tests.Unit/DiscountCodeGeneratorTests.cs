using GrpcDiscount.Application;
using FluentAssertions;
using NSubstitute;

namespace GrpcDiscountGenerator.Tests.Unit;

public sealed class DiscountCodeGeneratorTests
{
    private readonly DiscountCodeGenerator _discountCodeGenerator;
    private readonly IDiscountHelper _discountHelper;

    public DiscountCodeGeneratorTests()
    {
        this._discountHelper = Substitute.For<IDiscountHelper>();
        this._discountCodeGenerator = new DiscountCodeGenerator(this._discountHelper);
    }

    [Theory]
    [InlineData(3, 7, "ABCDEFG")]
    [InlineData(1, 8, "ABCDEFGH")]
    public async Task Should_GenerateDiscount_Successfully(int count, int length, string code)
    {
        // Arrange
        this._discountHelper.GenerateDiscount(length).Returns(code);

        // Act
        var result = await this._discountCodeGenerator.GenerateDiscountCodeAsync(count, length).ToListAsync();

        // Assert
        result.Should().HaveCount(count);
        result.Select(_ => _.Code.Value.Should().HaveLength(length));

        this._discountHelper.Received(count).GenerateDiscount(length);
    }
}
