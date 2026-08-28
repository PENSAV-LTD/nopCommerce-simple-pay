using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Payments.SimplePay.ViewModels;
public class SimplePayConfigurationModel
{
    [NopResourceDisplayName("Plugins.Payments.SimplePay.Fields.MerchantKey")]
    public string MerchantKey { get; set; }
    [NopResourceDisplayName("Plugins.Payments.SimplePay.Fields.DefaultCurrency")]
    [DataType(DataType.Currency)]
    public string DefaultCurrency { get; set; }
    [NopResourceDisplayName("Plugins.Payments.SimplePay.Fields.IsDefaultCurrencyUsed")]
    public bool IsDefaultCurrencyUsed { get; set; }
    [NopResourceDisplayName("Plugins.Payments.SimplePay.Fields.IsTwoStep")]
    public bool IsTwoStep { get; set; }
    [NopResourceDisplayName("Plugins.Payments.SimplePay.Fields.AdditionalFee")]
    public decimal AdditionalFee { get; set; }
    [NopResourceDisplayName("Plugins.Payments.SimplePay.Fields.RetentionPolicyInDay")]
    public int RetentionPolicyInDay { get; set; }
    [NopResourceDisplayName("Plugins.Payments.SimplePay.Fields.UseSandbox")]
    public bool UseSandbox { get; set; }
    [NopResourceDisplayName("Plugins.Payments.SimplePay.Fields.AddExtraPercentageToOrderTotal")]
    public decimal AddExtraPercentageToOrderTotal { get; set; }
    [NopResourceDisplayName("Plugins.Payments.SimplePay.Fields.AddExtraToOrderTotal")]
    public decimal AddExtraToOrderTotal { get; set; }
    [NopResourceDisplayName("Plugins.Payments.SimplePay.Fields.HasDetailedItems")]
    public bool HasDetailedItems { get; set; } = true;
    [NopResourceDisplayName("Plugins.Payments.SimplePay.Fields.OneItemName")]
    public string OneItemName { get; set; }
    [NopResourceDisplayName("Plugins.Payments.SimplePay.Fields.OtpIpnAddress")]
    public string OtpIpnAddress { get; set; }
}
