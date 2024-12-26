namespace Nop.Plugin.Payments.SimplePay.Processes;
public class SimplePaySandboxUrls : ISimplePayUrlsProvider
{
    public string StartUrl => "https://sandbox.simplepay.hu/payment/v2/start";
}
