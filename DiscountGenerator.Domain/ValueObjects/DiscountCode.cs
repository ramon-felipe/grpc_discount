﻿using CSharpFunctionalExtensions;

namespace GrpcDiscountGenerator.Domain.ValueObjects;

public sealed class DiscountCode
{
    private DiscountCode(string value)
    {
        this.Value = value;
    }

    public string Value { get; private set; }

    public static Result<DiscountCode> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Failure<DiscountCode>("Discount code cannot be null, empty or whitespace");

        if (!HasValidLength(value.Length))
            return Result.Failure<DiscountCode>("Invalid discount code length");

        return new DiscountCode(value);
    }

    public static DiscountCode CreateEmpty() => new (string.Empty);

    private static bool HasValidLength(int length)
    {
        int[] allowedCodeLengths = [7, 8];

        return allowedCodeLengths.Contains(length);
    }
}