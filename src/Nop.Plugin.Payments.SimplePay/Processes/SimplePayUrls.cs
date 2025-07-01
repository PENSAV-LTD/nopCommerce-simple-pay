namespace Nop.Plugin.Payments.SimplePay.Processes;
public class SimplePayUrls : ISimplePayUrlsProvider
{
    public const string START_URL = "https://simplepay.hu/payment/v2/start";
    public string StartUrl => START_URL;
}
