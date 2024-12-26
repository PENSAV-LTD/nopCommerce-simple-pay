namespace Nop.Plugin.Payments.SimplePay.Models;
public class BaseModel
{
    public string Salt { get; set; }
    public string Merchant { get; set; }
    public string OrderRef { get; set; }
}
