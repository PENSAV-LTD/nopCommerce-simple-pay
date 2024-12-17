using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Payments.SimplePay.Models;
internal enum PaymentMethods
{
    [Description("WIRE")]
    Wire,
    [Description("CARD")]
    Card
}
