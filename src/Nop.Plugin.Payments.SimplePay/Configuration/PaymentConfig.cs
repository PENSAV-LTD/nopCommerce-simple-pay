using System;

namespace Nop.Plugin.Payments.SimplePay.Configuration;
internal class PaymentConfig
{
    public TimeSpan Timeout { get; set; }
    public List<string> CountryToDeliver { get; set; }
}
