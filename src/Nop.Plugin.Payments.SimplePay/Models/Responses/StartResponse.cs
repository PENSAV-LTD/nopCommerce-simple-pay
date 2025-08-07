using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.SimplePay.Models.Responses;
public class StartResponse : TotalAndTimeoutBase
{
    [JsonPropertyName("transactionId")]
    public int TransactionId { get; set; }
    [JsonPropertyName("paymentUrl")]
    public string PaymentUrl { get; set; }
}
