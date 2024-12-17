namespace Nop.Plugin.Payments.SimplePay.Models.Responses;
internal class StartResponse : StartBase
{
    public string TransactionId { get; set; }
    public string PaymentUrl { get; set; }
}
