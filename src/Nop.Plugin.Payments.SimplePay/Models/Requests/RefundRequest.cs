namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
internal class RefundRequest : CurrencyBase
{
    public int RefundTotal { get; set; }
    public string SdkVersion { get; set; }
}
