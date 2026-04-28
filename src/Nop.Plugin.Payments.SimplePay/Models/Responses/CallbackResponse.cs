using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.SimplePay.Models.Responses;
public class CallbackResponse
{
    [JsonPropertyName("r")]
    public int ResponseCode { get; set; }
    [JsonPropertyName("t")]
    public long TransactionId { get; set; }
    [JsonPropertyName("e")]
    public string Event { get; set; }
    [JsonPropertyName("m")]
    public string Merchant { get; set; }
    [JsonPropertyName("o")]
    public string OrderRef { get; set; }
}
