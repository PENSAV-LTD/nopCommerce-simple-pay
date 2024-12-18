using Nop.Core.Configuration;

namespace Nop.Plugin.Payments.SimplePay.Settings;
public class SimplePaySettings : ISettings
{
    public string MerchantKey { get;set; }
    public string DefaultCurrency { get; set; }
    public string DefaultPaymentMethods { get; set; }

    public bool MaySelectEmail { get; set; }
    public bool MaySelectInvoice { get; set; }
    public List<string> MaySelectDelivery { get; set; }
    public bool IsTwoStep { get; set; }

    public string SdkVersion { get; set; }
}
