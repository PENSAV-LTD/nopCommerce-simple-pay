namespace Nop.Plugin.Payments.SimplePay.Exceptions;
public class SimplePayStateProvinceNotFoundException : SimplePayException
{
    public override string Message => "State/Province not found";
}
