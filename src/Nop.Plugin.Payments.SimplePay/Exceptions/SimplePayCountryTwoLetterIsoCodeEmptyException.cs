namespace Nop.Plugin.Payments.SimplePay.Exceptions;
public class SimplePayCountryTwoLetterIsoCodeEmptyException : SimplePayException
{
    public const string ERROR_MESSAGE = "Country two-letter ISO code is empty";
    public SimplePayCountryTwoLetterIsoCodeEmptyException() : base(ERROR_MESSAGE) { }
}
