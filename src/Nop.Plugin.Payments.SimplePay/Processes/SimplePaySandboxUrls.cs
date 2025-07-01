namespace Nop.Plugin.Payments.SimplePay.Processes;
public class SimplePaySandboxUrls : ISimplePayUrlsProvider
{
    public const string START_URL = "https://sandbox.simplepay.hu/payment/v2/start";
    public string StartUrl => START_URL;
}
