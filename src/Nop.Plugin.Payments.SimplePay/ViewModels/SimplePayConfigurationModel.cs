using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Payments.SimplePay.ViewModels;
public class SimplePayConfigurationModel
{
    [NopResourceDisplayName(Translations.Configuration.SIMPLE_PAY_FIELDS_MERCHANT)]
    public string Merchant { get; set; }
    [NopResourceDisplayName(Translations.Configuration.SIMPLE_PAY_FIELDS_MERCHANT_KEY)]
    public string MerchantKey { get; set; }
    [NopResourceDisplayName(Translations.Configuration.SIMPLE_PAY_FIELDS_DEFAULT_CURRENCY)]
    [DataType(DataType.Currency)]
    public string DefaultCurrency { get; set; }
    [NopResourceDisplayName(Translations.Configuration.SIMPLE_PAY_FIELDS_IS_DEFAULT_CURRENCY_USED)]
    public bool IsDefaultCurrencyUsed { get; set; }
    [NopResourceDisplayName(Translations.Configuration.SIMPLE_PAY_FIELDS_IS_TWO_STEP)]
    public bool IsTwoStep { get; set; }
    [NopResourceDisplayName(Translations.Configuration.SIMPLE_PAY_FIELDS_ADDITIONAL_FEE)]
    public decimal AdditionalFee { get; set; }
    [NopResourceDisplayName(Translations.Configuration.SIMPLE_PAY_FIELDS_RETENTION_POLICY_IN_DAY)]
    public int RetentionPolicyInDay { get; set; }
    [NopResourceDisplayName(Translations.Configuration.SIMPLE_PAY_FIELDS_USE_SANDBOX)]
    public bool UseSandbox { get; set; }
    [NopResourceDisplayName(Translations.Configuration.SIMPLE_PAY_FIELDS_ADD_EXTRA_PERCENTAGE_TO_ORDER_TOTAL)]
    public decimal AddExtraPercentageToOrderTotal { get; set; }
    [NopResourceDisplayName(Translations.Configuration.SIMPLE_PAY_FIELDS_ADD_EXTRA_TO_ORDER_TOTAL)]
    public decimal AddExtraToOrderTotal { get; set; }
    [NopResourceDisplayName(Translations.Configuration.SIMPLE_PAY_FIELDS_HAS_DETAILED_ITEMS)]
    public bool HasDetailedItems { get; set; } = true;
    [NopResourceDisplayName(Translations.Configuration.SIMPLE_PAY_FIELDS_ONE_ITEM_NAME)]
    public string OneItemName { get; set; }
    [NopResourceDisplayName(Translations.Configuration.SIMPLE_PAY_FIELDS_OTP_IPN_ADDRESS)]
    public string OtpIpnAddress { get; set; }
}
