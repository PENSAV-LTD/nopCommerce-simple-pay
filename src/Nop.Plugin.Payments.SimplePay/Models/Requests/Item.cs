using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
public class StartRequestItem
{
    [JsonPropertyName("ref")]
    public string Ref { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    [JsonPropertyName("tax")]
    public int Tax { get; set; }
}
