using Nop.Plugin.Payments.SimplePay.Processes;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Support;
public class SimplePayTestUrls : ISimplePayUrlsProvider
{
    public string StartUrl => "http://localhost:8080/"; //payment/v2/start/";
}
