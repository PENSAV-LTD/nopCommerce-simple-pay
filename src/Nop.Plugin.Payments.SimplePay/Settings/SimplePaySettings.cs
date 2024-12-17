using Nop.Core;

namespace Nop.Plugin.Payments.SimplePay.Settings;
public class SimplePaySettings : BaseEntity
{
    public string MerchantKey { get;set; }
    public string DefaultCurrency { get; set; }
    public string DefaultPaymentMethods { get; set; }
}
