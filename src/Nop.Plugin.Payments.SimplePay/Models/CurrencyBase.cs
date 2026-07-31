using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.SimplePay.Models;
public class CurrencyBase : BaseModel
{
    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}
