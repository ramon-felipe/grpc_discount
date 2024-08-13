using System.Runtime.CompilerServices;

namespace GrpcDiscount.Application;

public sealed class DiscountHelper : IDiscountHelper
{
    public string GenerateDiscount(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Shared.Next(s.Length)])
            .ToArray());
    }
}
