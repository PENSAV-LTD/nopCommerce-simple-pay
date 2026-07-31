using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.SimplePay.Models;
public class BaseModel
{
    [JsonPropertyName("salt")]
    public string Salt { get; set; }
    [JsonPropertyName("merchant")]
    public string Merchant { get; set; }
    [JsonPropertyName("orderRef")]
    public string OrderRef { get; set; }
}
