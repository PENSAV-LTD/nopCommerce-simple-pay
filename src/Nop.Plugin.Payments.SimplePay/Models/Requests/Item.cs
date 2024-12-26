namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
public class Item
{
    public string Ref { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public string Price { get; set; }
    public int Tax { get; set; }
}
