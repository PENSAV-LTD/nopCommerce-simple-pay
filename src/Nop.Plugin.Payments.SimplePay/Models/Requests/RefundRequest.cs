namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
public class RefundRequest : CurrencyBase
{
    public int RefundTotal { get; set; }
    public string SdkVersion { get; set; }
}
