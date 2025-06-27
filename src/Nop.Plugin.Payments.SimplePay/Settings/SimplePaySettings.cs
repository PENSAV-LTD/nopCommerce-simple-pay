using System.Net;
using Nop.Core.Configuration;
using Nop.Plugin.Payments.SimplePay.Models;

namespace Nop.Plugin.Payments.SimplePay.Settings;
public class SimplePaySettings : ISettings
{
    public string MerchantKey { get;set; }
    public string DefaultCurrency { get; set; }
    public PaymentMethods DefaultPaymentMethods { get; set; }
    public bool IsDefaultCurrencyUsed { get; set; }

    public bool IsTwoStep { get; set; }

    public string SdkVersion { get; set; }

    public decimal AdditionalFee { get;set; }
    public int RetentionPolicyInDay { get; set; }
    public bool UseSandbox { get; set; }
    public decimal AddExtraPercentageToOrderTotal { get; set; }
    public decimal AddExtraToOrderTotal { get; set; }

    public bool HasDetailedItems { get; set; }

    public IPAddress OtpIpnAddress { get; set; }
}
