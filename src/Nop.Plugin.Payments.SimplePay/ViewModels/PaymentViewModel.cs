using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Stores;

namespace Nop.Plugin.Payments.SimplePay.ViewModels;
public class PaymentViewModel
{
    public Store CurrentStore { get;set; }
    public Order Order { get; set; }
    public string PaymentUrl { get; set; }
}
