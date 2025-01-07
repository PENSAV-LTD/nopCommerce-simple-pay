using System.Net.Http.Headers;
using System.Text.Json;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Support;
using Nop.Plugin.Payments.SimplePay.Models.Requests;
using Nop.Plugin.Payments.SimplePay.Processes;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Plugin.Payments.SimplePay.Transactions;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers;
public class StartRequestDriver
{
    public const string DEFAULT_JSON_RESPONSE = "{\"salt\":\"KAC6ZRUacmQit98nFKOpjXgkwdC0Grzl\",\"merchant\":\"PUBLICTESTHUF\",\"orderRef\":\"101010515680292482600\",\"currency\":\"HUF\",\"transactionId\":99844942,\"timeout\":\"2019-09-11T21:14:08+02:00\",\"total\":25.0,\"paymentUrl\":\"https://sandbox.simplepay.hu/pay/pay/pspHU/8f4oKRec5R1B696xlxbOcj1jRhhABA2pwSLQDPW60zoGSDWzDU\"}";
    private readonly SimplePayPaymentProcessor _simplePayPaymentProcessor;
    private readonly SimplePaySettings _simplePaySettings;
    private readonly ISimplePayUrlsProvider _simplePayTestUrls;
    private readonly HttpClientFactorySettings _httpClientFactorySettings;
    private readonly FakeHttpClientFactory _fakeHttpClientFactory;

    public StartRequestDriver(
            SimplePayPaymentProcessor simplePayPaymentProcessor,
            SimplePaySettings simplePaySettings,
            ISimplePayUrlsProvider simplePayTestUrls,
            HttpClientFactorySettings httpClientFactorySettings,
            IHttpClientFactory fakeHttpClientFactory
        )
    {
        _simplePayPaymentProcessor = simplePayPaymentProcessor;
        _simplePaySettings = simplePaySettings;
        _simplePayTestUrls = simplePayTestUrls;
        _httpClientFactorySettings = httpClientFactorySettings;
        _fakeHttpClientFactory = fakeHttpClientFactory as FakeHttpClientFactory;
        _httpClientFactorySettings.Url = _simplePayTestUrls.StartUrl;
        _httpClientFactorySettings.ResponseBody = DEFAULT_JSON_RESPONSE;
    }

    public void SendStartRequest(Order order, string merchantKey = "TEST")
    {
        _simplePaySettings.MerchantKey = merchantKey;
        var postProcessPaymentRequest = PostProcessPaymentRequestCreator.Create(order);
        _simplePayPaymentProcessor.PostProcessPaymentAsync(postProcessPaymentRequest).GetAwaiter().GetResult();
    }

    public StartRequest GetStartRequest()
    {
        return JsonSerializer.Deserialize<StartRequest>(_fakeHttpClientFactory.RequestBody);
    }

    public HttpContentHeaders GetHeaders()
    {
        return _fakeHttpClientFactory.Headers;
    }
}
