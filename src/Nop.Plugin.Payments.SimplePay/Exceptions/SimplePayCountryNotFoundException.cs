namespace Nop.Plugin.Payments.SimplePay.Exceptions;
public class SimplePayCountryNotFoundException : SimplePayException
{
    public const string ERROR_MESSAGE = "Country not found";
    public SimplePayCountryNotFoundException() : base(ERROR_MESSAGE)
    {
    }
}
