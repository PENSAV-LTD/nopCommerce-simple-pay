namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
public class InvoiceDetail : AddressDetail
{
    public ThreeDSReqAuthMethod ThreeDSReqAuthMethod { get; set; }
}
