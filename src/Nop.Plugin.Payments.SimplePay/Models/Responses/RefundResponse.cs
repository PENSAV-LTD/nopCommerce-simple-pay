namespace Nop.Plugin.Payments.SimplePay.Models.Responses;
public class RefundResponse : CurrencyBase
{
    public string TransactionId { get; set; }
    public string RefundTransactionId { get; set; }
    public int RefundTotal { get; set; }
    public int RemainingTotal { get; set; }
}
