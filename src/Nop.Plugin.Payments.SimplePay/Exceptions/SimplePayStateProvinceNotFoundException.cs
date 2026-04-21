namespace Nop.Plugin.Payments.SimplePay.Exceptions;
public class SimplePayStateProvinceNotFoundException : SimplePayException
{
    public const string ERROR_MESSAGE = "State/Province not found";

    public SimplePayStateProvinceNotFoundException() : base(ERROR_MESSAGE) { }
}
