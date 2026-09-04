namespace Nop.Plugin.Payments.SimplePay.Translations;
public class Common
{
    public const string HINT = ".hint";
    public const string SIMPLE_PAY_TRANSACTION = "Nop.Plugin.Payments.SimplePay.Common.Transaction";

    public static readonly IDictionary<string, string> EnglishTranslation = new Dictionary<string, string>()
    {
        {SIMPLE_PAY_TRANSACTION, "SimplePay transaction ID: {0}" },
    };

    public static readonly IDictionary<string, string> HungarianTranslation = new Dictionary<string, string>()
    {
        {SIMPLE_PAY_TRANSACTION, "SimplePay tranzakció azonosító: {0}" },
    };

}
