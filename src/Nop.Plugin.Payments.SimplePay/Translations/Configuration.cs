namespace Nop.Plugin.Payments.SimplePay.Translations;
public class Configuration
{
    public const string SIMPLE_PAY_ONBOARDING_TITLE = "Plugins.Payments.SimplePay.Onboarding.Title";
    public const string SIMPLE_PAY_ONBOARDING_DESCRIPTION = "Plugins.Payments.SimplePay.Onboarding.Description";

    public const string SIMPLE_PAY_FIELDS_MERCHANT_KEY = "Plugins.Payments.SimplePay.Fields.MerchantKey";
    public const string SIMPLE_PAY_FIELDS_DEFAULT_CURRENCY = "Plugins.Payments.SimplePay.Fields.DefaultCurrency";
    public const string SIMPLE_PAY_FIELDS_IS_DEFAULT_CURRENCY_USED = "Plugins.Payments.SimplePay.Fields.IsDefaultCurrencyUsed";
    public const string SIMPLE_PAY_FIELDS_IS_TWO_STEP = "Plugins.Payments.SimplePay.Fields.IsTwoStep";
    public const string SIMPLE_PAY_FIELDS_ADDITIONAL_FEE = "Plugins.Payments.SimplePay.Fields.AdditionalFee";
    public const string SIMPLE_PAY_FIELDS_RETENTION_POLICY_IN_DAY = "Plugins.Payments.SimplePay.Fields.RetentionPolicyInDay";
    public const string SIMPLE_PAY_FIELDS_USE_SANDBOX = "Plugins.Payments.SimplePay.Fields.UseSandbox";
    public const string SIMPLE_PAY_FIELDS_ADD_EXTRA_PERCENTAGE_TO_ORDER_TOTAL = "Plugins.Payments.SimplePay.Fields.AddExtraPercentageToOrderTotal";
    public const string SIMPLE_PAY_FIELDS_ADD_EXTRA_TO_ORDER_TOTAL = "Plugins.Payments.SimplePay.Fields.AddExtraToOrderTotal";
    public const string SIMPLE_PAY_FIELDS_HAS_DETAILED_ITEMS = "Plugins.Payments.SimplePay.Fields.HasDetailedItems";
    public const string SIMPLE_PAY_FIELDS_ONE_ITEM_NAME = "Plugins.Payments.SimplePay.Fields.OneItemName";
    public const string SIMPLE_PAY_FIELDS_OTP_IPN_ADDRESS = "Plugins.Payments.SimplePay.Fields.OtpIpnAddress";

    public IDictionary<string, string> EnglishTranslation = new Dictionary<string, string>()
    {
        {SIMPLE_PAY_ONBOARDING_TITLE, "Connect SimplePay account" },
        {SIMPLE_PAY_ONBOARDING_DESCRIPTION, "To use SimplePay payment method, you need to connect your SimplePay account. Please click the button below to start the onboarding process." },
        {SIMPLE_PAY_FIELDS_MERCHANT_KEY, "Merchant Key" },
        {SIMPLE_PAY_FIELDS_DEFAULT_CURRENCY, "Default Currency" },
        {SIMPLE_PAY_FIELDS_IS_DEFAULT_CURRENCY_USED, "Is Default Currency Used" },
        {SIMPLE_PAY_FIELDS_IS_TWO_STEP, "Is Two Step" },
        {SIMPLE_PAY_FIELDS_ADDITIONAL_FEE, "Additional Fee" },
        {SIMPLE_PAY_FIELDS_RETENTION_POLICY_IN_DAY, "Retention Policy In Day" },
        {SIMPLE_PAY_FIELDS_USE_SANDBOX, "Use Sandbox" },
        {SIMPLE_PAY_FIELDS_ADD_EXTRA_PERCENTAGE_TO_ORDER_TOTAL, "Add Extra Percentage To Order Total" },
        {SIMPLE_PAY_FIELDS_ADD_EXTRA_TO_ORDER_TOTAL, "Add Extra To Order Total" },
        {SIMPLE_PAY_FIELDS_HAS_DETAILED_ITEMS, "Has Detailed Items" },
        {SIMPLE_PAY_FIELDS_ONE_ITEM_NAME, "One Item Name" },
        {SIMPLE_PAY_FIELDS_OTP_IPN_ADDRESS, "OTP IPN Address" },
    };

    public IDictionary<string, string> HungarianTranslation = new Dictionary<string, string>()
    {
        {SIMPLE_PAY_ONBOARDING_TITLE, "Csatlakozás a SimplePay fiókhoz" },
        {SIMPLE_PAY_ONBOARDING_DESCRIPTION, "A SimplePay fizetési mód használatához csatlakoztatnia kell a SimplePay fiókját. Kérjük, kattintson az alábbi gombra az onboarding folyamat elindításához." },
        {SIMPLE_PAY_FIELDS_MERCHANT_KEY, "Kereskedői kulcs" },
        {SIMPLE_PAY_FIELDS_DEFAULT_CURRENCY, "Alapértelmezett pénznem" },
        {SIMPLE_PAY_FIELDS_IS_DEFAULT_CURRENCY_USED, "Alapértelmezett pénznem használata" },
        {SIMPLE_PAY_FIELDS_IS_TWO_STEP, "Kétlépcsős fizetés" },
        {SIMPLE_PAY_FIELDS_ADDITIONAL_FEE, "További díj" },
        {SIMPLE_PAY_FIELDS_RETENTION_POLICY_IN_DAY, "Megőrzési politika napokban" },
        {SIMPLE_PAY_FIELDS_USE_SANDBOX, "Sandbox használata" },
        {SIMPLE_PAY_FIELDS_ADD_EXTRA_PERCENTAGE_TO_ORDER_TOTAL, "Extra százalék hozzáadása a rendelés összegéhez" },
        {SIMPLE_PAY_FIELDS_ADD_EXTRA_TO_ORDER_TOTAL, "Extra hozzáadása a rendelés összegéhez" },
        {SIMPLE_PAY_FIELDS_HAS_DETAILED_ITEMS, "Részletes tételek" },
        {SIMPLE_PAY_FIELDS_ONE_ITEM_NAME, "Egy tétel neve" },
        {SIMPLE_PAY_FIELDS_OTP_IPN_ADDRESS, "OTP IPN cím" },
    };

}
