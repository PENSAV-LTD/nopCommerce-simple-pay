namespace Nop.Plugin.Payments.SimplePay.Exceptions;
public class SimplePayStateProvinceNameIsEmptyException : SimplePayException
{
    public const string ERROR_MESSAGE = "State/Province name is empty";
    public SimplePayStateProvinceNameIsEmptyException() : base(ERROR_MESSAGE) { }
}
