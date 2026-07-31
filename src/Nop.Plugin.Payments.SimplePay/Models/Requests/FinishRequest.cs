namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
public class FinishRequest : CurrencyBase
{
    public int OriginalTotal { get; set; }
    public int ApprovedTotal { get; set; }
    public string SdkVersion { get; set; }
}
