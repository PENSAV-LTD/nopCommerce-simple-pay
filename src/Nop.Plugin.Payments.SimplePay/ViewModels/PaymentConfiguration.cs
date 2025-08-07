using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Stores;

namespace Nop.Plugin.Payments.SimplePay.ViewModels;
public class PaymentConfiguration
{
    public Order Order { get; set; }
    public Store Store { get; set; }
    public string PaymentUrl { get; set; }
}
