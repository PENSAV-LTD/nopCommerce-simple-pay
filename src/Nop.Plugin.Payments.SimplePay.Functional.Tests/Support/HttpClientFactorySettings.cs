using System.Net;
using System.Net.Http.Headers;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Support;
public class HttpClientFactorySettings
{
    public string Url { get; set; }  
    public string ResponseBody { get; set; }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public HttpResponseHeaders Headers { get; set; }
}
