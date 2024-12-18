namespace Nop.Plugin.Payments.SimplePay.Translations;
public class Callback
{
    public const string CANCELLATION_HEADER = "Nop.Plugin.Payments.SimplePay.Callback.Cancel.Header";
    public const string TIMEOUT_HEADER = "Nop.Plugin.Payments.SimplePay.Callback.Timeout.Header";
    public const string FAILED_HEADER = "Nop.Plugin.Payments.SimplePay.Callback.Fail.Header";
    public const string FAILED_MESSAGE = "Nop.Plugin.Payments.SimplePay.Callback.Fail.Message";
    public const string SUCCESS_HEADER = "Nop.Plugin.Payments.SimplePay.Callback.Success.Header";

    public IDictionary<string, string> EnglishTranslation = new Dictionary<string, string>()
    {
        {CANCELLATION_HEADER, "Payment has cancelled" },
        {TIMEOUT_HEADER, "Timeout" },
        {FAILED_HEADER, "Transaction has failed" },
        {FAILED_MESSAGE, "Please check the correctness of the data entered during the transaction.\r\nIf you have entered all the data correctly, the refusal\r\nin order to investigate the cause, please contact us\r\nwith your card issuing bank." },
        {SUCCESS_HEADER, "Successful transaction" },
    };

    public IDictionary<string, string> HungarianTranslation = new Dictionary<string, string>()
    {
        {CANCELLATION_HEADER, "Megszakított fizetés" },
        {TIMEOUT_HEADER, "Időtúllépés" },
        {FAILED_HEADER, "Sikertelen tranzakció" },
        {FAILED_MESSAGE, "Kérjük, ellenőrizze a tranzakció során megadott adatok helyességét.\r\nAmennyiben minden adatot helyesen adott meg, a visszautasítás\r\nokának kivizsgálása érdekében kérjük, szíveskedjen kapcsolatba lépni\r\nkártyakibocsátó bankjával." },
        {SUCCESS_HEADER, "Sikeres tranzakció" },
    };
}
