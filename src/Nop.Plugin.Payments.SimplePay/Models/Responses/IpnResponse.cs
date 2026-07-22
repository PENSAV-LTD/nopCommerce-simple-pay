using System.Text.Json.Serialization;
using Nop.Plugin.Payments.SimplePay.Models.Requests;

namespace Nop.Plugin.Payments.SimplePay.Models.Responses;
public class IpnResponse : IpnRequest
{
    public IpnResponse(IpnRequest request, DateTime receiveDate)
    {
        // Copy properties from the request
        foreach (var property in typeof(IpnRequest).GetProperties())
        {
            if (property.CanRead && property.CanWrite)
            {
                property.SetValue(this, property.GetValue(request));
            }
        }

        ReceiveDate = receiveDate;
    }

    [JsonPropertyName("receiveDate")]
    public DateTime ReceiveDate { get; set; }
}
