using GrpcDiscount.Application;
using FluentAssertions;
using NSubstitute;
using GrpcDiscount.Application.Interfaces;
using GrpcDiscountGenerator.Domain;
using GrpcDiscountGenerator.Infrastructure.Repositories;
using CSharpFunctionalExtensions;

namespace GrpcDiscountGenerator.Tests.Unit;

public sealed class DiscountCodeGeneratorTests
{
    private readonly DiscountCodeGenerator _discountCodeGenerator;
    private readonly IDiscountHelper _discountHelper;
    private readonly IRepository<Discount> _repository;

    public DiscountCodeGeneratorTests()
    {
        this._discountHelper = Substitute.For<IDiscountHelper>();
        this._repository = Substitute.For<IRepository<Discount>>();
        this._discountCodeGenerator = new DiscountCodeGenerator(this._discountHelper, this._repository);
    }

    [Theory]
    [InlineData(3, 7, "ABCDEFG")]
    [InlineData(1, 8, "ABCDEFGH")]
    public async Task Should_GenerateDiscount_Successfully(int count, int length, string code)
    {
        // Arrange
        this._discountHelper.GenerateDiscount(length).Returns(code);
        this._repository.Get(Arg.Any<Func<Discount, bool>>()).Returns(Maybe<Discount>.None);

        // Act
        var result = await this._discountCodeGenerator.GenerateDiscountCodeAsync(count, length).ToListAsync();

        // Assert
        result.Should().HaveCount(count);
        result.Select(_ => _.Code.Value.Should().HaveLength(length));

        this._discountHelper.Received(count).GenerateDiscount(length);
    }
}
