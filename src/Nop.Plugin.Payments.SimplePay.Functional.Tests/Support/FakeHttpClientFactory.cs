using System.Net;
using System.Net.Http.Headers;
using Moq;
using Moq.Protected;
using Nop.Plugin.Payments.SimplePay.Messages.Validators;
using Nop.Plugin.Payments.SimplePay.Settings;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Support;
public class FakeHttpClientFactory : IHttpClientFactory
{
    private readonly IMessageToSendValidator _messageToSendValidator;
    private readonly SimplePaySettings _simplePaySettings;

    public HttpClientFactorySettings Settings { get; private set; }
    public Uri Url { get; private set; }
    public string RequestBody { get; private set; }
    public HttpContentHeaders Headers { get; private set; }
    public HttpResponseMessage ResponseMessage { get; private set; }

    public FakeHttpClientFactory(
        HttpClientFactorySettings settings,
        IMessageToSendValidator messageToSendValidator,
        SimplePaySettings simplePaySettings
        )
    {
        Settings = settings;
        _messageToSendValidator = messageToSendValidator;
        _simplePaySettings = simplePaySettings;
    }

    public HttpClient CreateClient(string name)
    {
        ResponseMessage = new HttpResponseMessage()
        {
            StatusCode = Settings.StatusCode,
            Content = new StringContent(Settings.ResponseBody)
        };
        ResponseMessage.Headers.Add("Signature", _messageToSendValidator.CalculateSignature(_simplePaySettings.MerchantKey, Settings.ResponseBody));
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Callback<HttpRequestMessage, CancellationToken>((request, cancellationToken) =>
            {
                Url = request.RequestUri;
                Headers = request.Content.Headers;
                RequestBody = request.Content.ReadAsStringAsync().Result;
            })
            .ReturnsAsync(ResponseMessage);

        return new HttpClient(mockMessageHandler.Object);
    }
}
