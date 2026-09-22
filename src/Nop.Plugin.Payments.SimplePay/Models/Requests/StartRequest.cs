using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
public class StartRequest : TotalAndTimeoutBase
{
    [JsonPropertyName("customerEmail")]
    public string CustomerEmail { get; set; }
    [JsonPropertyName("language")]
    public string Language { get; set; }
    [JsonPropertyName("sdkVersion")]
    public string SdkVersion { get; set; }
    [JsonPropertyName("methods")]
    public List<string> Methods { get; set; }
    [JsonPropertyName("url")]
    public string Url { get; set; }
    [JsonPropertyName("urls")]
    public Urls Urls { get; set; }
    [JsonPropertyName("invoice")]
    public InvoiceDetail Invoice { get; set; }
    [JsonPropertyName("maySelectEmail")]
    public bool MaySelectEmail { get; set; }
    [JsonPropertyName("maySelectInvoice")]
    public bool MaySelectInvoice { get; set; }
    [JsonPropertyName("items")]
    public List<StartRequestItem> Items { get; set; }
    [JsonPropertyName("shippingCost")]
    public int ShippingCost { get; set; }
    [JsonPropertyName("discount")]
    public int Discount { get; set; }
    [JsonPropertyName("customer")]
    public string Customer { get; set; }
    [JsonPropertyName("twoStep")]
    public bool TwoStep { get; set; }
    [JsonPropertyName("delivery")]
    public AddressDetail Delivery { get; set; }
    [JsonPropertyName("maySelectDelivery")]
    public List<string> MaySelectDelivery { get; set; }
}
