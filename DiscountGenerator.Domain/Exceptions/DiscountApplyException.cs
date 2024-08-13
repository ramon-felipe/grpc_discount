namespace GrpcDiscountGenerator.Domain.Exceptions;
public sealed class DiscountApplyException : Exception
{
    public DiscountApplyException(string message): base(message)
    {
    }
}
