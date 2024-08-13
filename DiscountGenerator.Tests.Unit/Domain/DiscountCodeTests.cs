using FluentAssertions;
using GrpcDiscountGenerator.Domain.ValueObjects;

namespace DiscountGenerator.Tests.Unit.Domain;

public sealed class DiscountCodeTests
{
	[Theory]
	[InlineData("abcdefg")]
	public void Should_CreateDiscountCode_Successfully(string code)
	{
		// Act
		var discountCodeResult = DiscountCode.Create(code);

		// Assert
		discountCodeResult.Should().Succeed();
		var discountCode = discountCodeResult.Value;

		discountCode.Value.Should().Be(code);
	}

	[Fact]
	public void Should_CreateEmptyDiscountCode_Successfully()
	{
        // Act
        var discountCode = DiscountCode.CreateEmpty();

		// Assert
		discountCode.Should().NotBeNull();
		discountCode.Value.Should().BeEmpty();
    }
}

