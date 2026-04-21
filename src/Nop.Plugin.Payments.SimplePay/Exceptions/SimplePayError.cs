namespace Nop.Plugin.Payments.SimplePay.Exceptions;
public class SimplePayError : SimplePayException
{   
    public const string ERROR_MESSAGE = "SimplePay error";
    public List<int> ErrorCodes { get; }
    public SimplePayError(List<int> errorCodes)
        : base(ERROR_MESSAGE)
    {
        ErrorCodes = errorCodes ?? throw new ArgumentNullException(nameof(errorCodes));
    }
    public override string Message => $"SimplePay error: {string.Join(", ", ErrorCodes)}";
}
