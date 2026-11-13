using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.SimplePay.Models;
public class BaseModel
{
    [JsonPropertyName("salt")]
    [JsonPropertyOrder(0)]
    public string Salt { get; set; }
    [JsonPropertyName("merchant")]
    [JsonPropertyOrder(3)]
    public string Merchant { get; set; }
    [JsonPropertyName("orderRef")]
    [JsonPropertyOrder(1)]
    public string OrderRef { get; set; }
}
