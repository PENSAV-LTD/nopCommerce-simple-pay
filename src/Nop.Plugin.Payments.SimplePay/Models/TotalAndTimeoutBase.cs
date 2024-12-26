namespace Nop.Plugin.Payments.SimplePay.Models;
public class TotalAndTimeoutBase : CurrencyBase
{
    public DateTime Timeout { get; set; }
    public int Total { get; set; }
}
