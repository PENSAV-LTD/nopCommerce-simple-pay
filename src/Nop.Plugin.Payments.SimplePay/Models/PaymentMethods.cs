using System.ComponentModel;

namespace Nop.Plugin.Payments.SimplePay.Models;
public enum PaymentMethods
{
    [Description("WIRE")]
    Wire,
    [Description("CARD")]
    Card
}
