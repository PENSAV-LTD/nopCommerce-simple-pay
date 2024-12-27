using System.Net;
using System.Net.Http.Headers;
using Moq;
using Moq.Protected;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Support;
public class FakeHttpClientFactory : IHttpClientFactory
{
    private readonly HttpClientFactorySettings _settings;
    public Uri Url { get; private set; }
    public string RequestBody { get; private set; }
    public HttpContentHeaders Headers { get; private set; }

    public FakeHttpClientFactory(
        HttpClientFactorySettings settings)
    {
        _settings = settings;
    }

    public HttpClient CreateClient(string name)
    {
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .Callback<HttpRequestMessage, CancellationToken>((request, cancellationToken) =>
            {
                Url = request.RequestUri;
                Headers = request.Content.Headers;
                RequestBody = request.Content.ReadAsStringAsync().Result;
            })
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(_settings.ResponseBody)
            });

        return new HttpClient(mockMessageHandler.Object);
    }
}
