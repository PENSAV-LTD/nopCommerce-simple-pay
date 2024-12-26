namespace Nop.Plugin.Payments.SimplePay.Models.Responses;
public class IpnResponse : BaseModel
{
    public PaymentMethods Method { get; set; }
    public DateTime FinishDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public string TransactionId { get; set; }
    public string Status { get; set; }
}
