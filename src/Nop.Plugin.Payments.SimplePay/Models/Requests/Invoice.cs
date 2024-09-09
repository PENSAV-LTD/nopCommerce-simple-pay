namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
internal class InvoiceDetail : AddressDetail
{
    public ThreeDSReqAuthMethod ThreeDSReqAuthMethod { get; set; }
}
