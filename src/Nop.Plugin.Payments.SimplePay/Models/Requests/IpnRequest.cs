using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
public class IpnRequest : BaseModel
{
    [JsonPropertyName("method")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentMethods Method { get; set; }
    [JsonPropertyName("finishDate")]
    public DateTime FinishDate { get; set; }
    [JsonPropertyName("paymentDate")]
    public DateTime PaymentDate { get; set; }
    [JsonPropertyName("transactionId")]
    public long TransactionId { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
}
