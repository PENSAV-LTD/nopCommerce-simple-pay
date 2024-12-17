namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
internal class StartRequest : StartBase
{
    public string CustomerEmail { get; set; }
    public string Language { get; set; }
    public string SdkVersion { get; set; }
    public List<string> Methods { get; set; }
    public string Url { get; set; }
    public Urls Urls { get; set; }
    public InvoiceDetail Invoice { get; set; }
    public bool MaySelectEmail { get; set; }
    public bool MaySelectInvoice { get; set; }
    public List<Item> Items { get; set; }
    public int ShippingCost { get; set; }
    public int Discount { get; set; }
    public string Customer { get; set; }
    public bool TwoStep { get; set; }
    public AddressDetail Delivery { get; set; }
    public List<string> MaySelectDelivery { get; set; }
}
