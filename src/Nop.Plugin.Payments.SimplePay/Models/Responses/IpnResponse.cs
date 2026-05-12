using System.Text.Json.Serialization;
using Nop.Plugin.Payments.SimplePay.Models.Requests;

namespace Nop.Plugin.Payments.SimplePay.Models.Responses;
public class IpnResponse : IpnRequest
{
    [JsonPropertyName("receiveDate")]
    public DateTime ReceiveDate { get; set; }
}
