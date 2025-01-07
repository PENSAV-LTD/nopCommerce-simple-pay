namespace Nop.Plugin.Payments.SimplePay.Exceptions;
public class SimplePayStateProvinceNameIsEmptyException : SimplePayException
{
    public override string Message => "State/Province name is empty";
}
