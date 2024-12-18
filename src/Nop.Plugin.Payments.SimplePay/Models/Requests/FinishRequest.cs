namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
internal class FinishRequest : CurrencyBase
{
    public int OriginalTotal { get; set; }
    public int ApprovedTotal { get; set; }
    public string SdkVersion { get; set; }
}
