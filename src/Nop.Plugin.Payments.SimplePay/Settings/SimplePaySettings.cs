using System.Net;
using Nop.Core.Configuration;
using Nop.Plugin.Payments.SimplePay.Models;
using Nop.Plugin.Payments.SimplePay.Processes;

namespace Nop.Plugin.Payments.SimplePay.Settings;
public class SimplePaySettings : ISettings
{
    public const string SDK_VERSION = "SimplePayV2.1_Payment_NopCommerce_by_pensav_1.0.0.0";
    public const PaymentMethods DEFAULT_PAYMENT_METHODS = PaymentMethods.Card;
    public string Merchant { get; set; }
    public string MerchantKey { get; set; }
    public string DefaultCurrency { get; set; } = "HUF";
    public string DefaultLanguage { get; set; } = "HU";
    public PaymentMethods DefaultPaymentMethods { get; set; } = DEFAULT_PAYMENT_METHODS;
    public bool IsDefaultCurrencyUsed { get; set; } = true;
    public bool IsTwoStep { get; set; } = false;
    public string SdkVersion { get; set; } = SDK_VERSION;
    public decimal AdditionalFee { get;set; } = 0;
    public int RetentionPolicyInDay { get; set; } = 90;
    public bool UseSandbox { get; set; } = true;
    public decimal AddExtraPercentageToOrderTotal { get; set; } = 0;
    public decimal AddExtraToOrderTotal { get; set; } = 0;
    public bool HasDetailedItems { get; set; } = true;
    public string OneItemName { get; set; }
    public Uri OtpIpnAddress { get; set; } = new Uri(SimplePaySandboxUrls.START_URL);
}
