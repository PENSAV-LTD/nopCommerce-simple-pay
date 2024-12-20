using System.ComponentModel;

namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
public enum PaymentMethods
{
    [Description("CARD")]
    Card = 1,
    [Description("WIRE")]
    Wire = 2,
}
