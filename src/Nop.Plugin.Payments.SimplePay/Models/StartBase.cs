namespace Nop.Plugin.Payments.SimplePay.Models;
internal class StartBase : BaseModel
{
    public string Currency { get; set; }
    public DateTime Timeout { get; set; }
    public int Total { get; set; }
}
