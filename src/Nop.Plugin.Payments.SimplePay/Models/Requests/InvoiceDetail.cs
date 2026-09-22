using System.Text.Json.Serialization;

namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
public class InvoiceDetail : AddressDetail
{
    [JsonPropertyName("threeDSReqAuthMethod")]
    public ThreeDSReqAuthMethod ThreeDSReqAuthMethod { get; set; }
}
