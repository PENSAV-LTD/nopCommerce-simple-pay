using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.SimplePay.Models;
public class TotalAndTimeoutBase : CurrencyBase
{
    [JsonPropertyName("timeout")]
    public DateTime Timeout { get; set; }
    [JsonPropertyName("total")]
    public decimal Total { get; set; }
}
