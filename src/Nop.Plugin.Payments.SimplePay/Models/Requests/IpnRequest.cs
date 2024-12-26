using Nop.Plugin.Payments.SimplePay.Models.Responses;

namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
public class IpnRequest : IpnResponse
{
    public DateTime ReceiveDate { get; set; }
}
