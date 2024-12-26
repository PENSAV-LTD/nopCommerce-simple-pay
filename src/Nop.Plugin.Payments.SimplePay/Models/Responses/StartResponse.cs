namespace Nop.Plugin.Payments.SimplePay.Models.Responses;
public class StartResponse : TotalAndTimeoutBase
{
    public string TransactionId { get; set; }
    public string PaymentUrl { get; set; }
}
