using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
public class AddressDetail
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("company")]
    public string Company { get; set; }
    [JsonPropertyName("country")]
    public string Country { get; set; }
    [JsonPropertyName("state")]
    public string State { get; set; }
    [JsonPropertyName("city")]
    public string City { get; set; }
    [JsonPropertyName("zip")]
    public string Zip { get; set; }
    [JsonPropertyName("address")]
    public string Address { get; set; }
    [JsonPropertyName("address2")]
    public string Address2 { get; set; }
    [JsonPropertyName("phone")]
    public string Phone { get; set; }
}
