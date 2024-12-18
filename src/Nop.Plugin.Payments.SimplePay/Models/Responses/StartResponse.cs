namespace Nop.Plugin.Payments.SimplePay.Models.Responses;
internal class StartResponse : TotalAndTimeoutBase
{
    public string TransactionId { get; set; }
    public string PaymentUrl { get; set; }
}
