namespace Nop.Plugin.Payments.SimplePay.Exceptions;
public class SimplePayError : SimplePayException
{   
    public List<int> ErrorCodes { get; }
    public SimplePayError(List<int> errorCodes)
    {
        ErrorCodes = errorCodes ?? throw new ArgumentNullException(nameof(errorCodes));
    }
    public override string Message => $"SimplePay error: {string.Join(", ", ErrorCodes)}";
}
