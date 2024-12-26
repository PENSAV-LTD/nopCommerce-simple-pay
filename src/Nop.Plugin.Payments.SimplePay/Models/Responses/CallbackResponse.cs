using Newtonsoft.Json;

namespace Nop.Plugin.Payments.SimplePay.Models.Responses;
public class CallbackResponse
{
    [JsonProperty("r")]
    public string ResponseCode { get; set; }
    [JsonProperty("t")]
    public string TransactionId { get; set; }
    [JsonProperty("e")]
    public string Event { get; set; }
    [JsonProperty("m")]
    public string Merchant { get; set; }
    [JsonProperty("o")]
    public string OrderRef { get; set; }
}
