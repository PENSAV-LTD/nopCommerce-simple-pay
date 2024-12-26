namespace Nop.Plugin.Payments.SimplePay.Processes;
public class SimplePayUrls : ISimplePayUrlsProvider
{
    public string StartUrl => "https://simplepay.hu/payment/v2/start";
}
