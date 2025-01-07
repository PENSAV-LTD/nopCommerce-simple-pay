namespace Nop.Plugin.Payments.SimplePay.Exceptions;
public class SimplePayCountryNotFoundException : SimplePayException
{
    public override string Message => "Country not found";
}
