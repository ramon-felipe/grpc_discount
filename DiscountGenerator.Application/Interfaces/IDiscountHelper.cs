﻿using CSharpFunctionalExtensions;
using GrpcDiscountGenerator.Domain;

namespace GrpcDiscount.Application.Interfaces;

public interface IDiscountHelper
{
    string GenerateDiscount(int length);
    IMaybe<Discount> GetLast();
}
