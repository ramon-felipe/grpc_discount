using GrpcDiscount.Application;
using FluentAssertions;
using NSubstitute;
using GrpcDiscount.Application.Interfaces;
using GrpcDiscountGenerator.Domain;
using GrpcDiscountGenerator.Infrastructure.Repositories;
using CSharpFunctionalExtensions;

namespace DiscountGenerator.Tests.Unit.Application;

public sealed class DiscountCodeGeneratorTests
{
    private readonly DiscountCodeGenerator _discountCodeGenerator;
    private readonly IDiscountHelper _discountHelper;
    private readonly IRepository<Discount> _repository;

    public DiscountCodeGeneratorTests()
    {
        _discountHelper = Substitute.For<IDiscountHelper>();
        _repository = Substitute.For<IRepository<Discount>>();
        _discountCodeGenerator = new DiscountCodeGenerator(_discountHelper, _repository);
    }

    [Theory]
    [InlineData(3, 7, "ABCDEFG")]
    [InlineData(1, 8, "ABCDEFGH")]
    public async Task Should_GenerateDiscount_Successfully(int count, int length, string code)
    {
        // Arrange
        _discountHelper.GenerateDiscount(length).Returns(code);
        _repository.Get(Arg.Any<Func<Discount, bool>>()).Returns(Maybe<Discount>.None);

        // Act
        var result = await _discountCodeGenerator.GenerateCodesAsync(count, length);

        // Assert
        result.Should().HaveCount(count);
        result.Select(_ => _.Code.Value.Should().HaveLength(length));

        _discountHelper.Received(count).GenerateDiscount(length);
    }
}
