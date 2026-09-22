using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
public class Urls
{
    [JsonPropertyName("success")]
    public string Success { get; set; }
    [JsonPropertyName("fail")]
    public string Fail { get; set; }
    [JsonPropertyName("cancel")]
    public string Cancel { get; set; }
    [JsonPropertyName("timeout")]
    public string Timeout { get; set; }
}
