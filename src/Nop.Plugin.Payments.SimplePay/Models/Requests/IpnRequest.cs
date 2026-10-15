using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
public class IpnRequest : BaseModel
{
    [JsonPropertyName("method")]
    [JsonPropertyOrder(2)]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentMethods Method { get; set; }
    [JsonPropertyOrder(4)]
    [JsonPropertyName("finishDate")]
    public DateTime FinishDate { get; set; }
    [JsonPropertyOrder(5)]
    [JsonPropertyName("paymentDate")]
    public DateTime PaymentDate { get; set; }
    [JsonPropertyOrder(6)]
    [JsonPropertyName("transactionId")]
    public long TransactionId { get; set; }
    [JsonPropertyName("status")]
    [JsonPropertyOrder(7)]
    public string Status { get; set; }
}
