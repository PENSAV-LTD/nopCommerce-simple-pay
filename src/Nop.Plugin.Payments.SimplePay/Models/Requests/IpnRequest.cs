using Nop.Plugin.Payments.SimplePay.Models.Responses;

namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
internal class IpnRequest : IpnResponse
{
    public DateTime ReceiveDate { get; set; }
}
