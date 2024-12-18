namespace Nop.Plugin.Payments.SimplePay.Models;
internal class TotalAndTimeoutBase : CurrencyBase
{
    public DateTime Timeout { get; set; }
    public int Total { get; set; }
}
