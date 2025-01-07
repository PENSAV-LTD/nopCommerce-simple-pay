namespace Nop.Plugin.Payments.SimplePay.Exceptions;
public class SimplePayCountryTwoLetterIsoCodeEmptyException : SimplePayException
{
    public override string Message => "Country two-letter ISO code is empty";
}
