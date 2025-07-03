using System.Net;
using Nop.Core.Configuration;
using Nop.Plugin.Payments.SimplePay.Models;

namespace Nop.Plugin.Payments.SimplePay.Settings;
public class SimplePaySettings : ISettings
{
    public const string SDK_VERSION = "SimplePayV2.1_Payment_NopCommerce_by_tmsblzs_1.0.0.0";
    public const PaymentMethods DEFAULT_PAYMENT_METHODS = PaymentMethods.Card;
    public string MerchantKey { get;set; }
    public string DefaultCurrency { get; set; }
    public PaymentMethods DefaultPaymentMethods { get; set; } = DEFAULT_PAYMENT_METHODS;
    public bool IsDefaultCurrencyUsed { get; set; }
    public bool IsTwoStep { get; set; }
    public string SdkVersion { get; set; } = SDK_VERSION;
    public decimal AdditionalFee { get;set; }
    public int RetentionPolicyInDay { get; set; }
    public bool UseSandbox { get; set; }
    public decimal AddExtraPercentageToOrderTotal { get; set; }
    public decimal AddExtraToOrderTotal { get; set; }
    public bool HasDetailedItems { get; set; } = true;
    public string OneItemName { get; set; }
    public IPAddress OtpIpnAddress { get; set; }
}
