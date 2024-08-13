using System.Runtime.CompilerServices;
using CSharpFunctionalExtensions;
using GrpcDiscount.Application.Interfaces;
using GrpcDiscountGenerator.Domain;
using GrpcDiscountGenerator.Infrastructure.Repositories;

namespace GrpcDiscount.Application;

public sealed class DiscountHelper : IDiscountHelper
{
    private readonly IRepository<Discount> _repository;

    public DiscountHelper(IRepository<Discount> repository)
    {
        this._repository = repository;
    }

    public string GenerateDiscount(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Shared.Next(s.Length)])
            .ToArray());
    }

    public IMaybe<Discount> GetLast() => this._repository.GetLast();
}
