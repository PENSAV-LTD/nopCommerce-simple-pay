using System.ComponentModel;

namespace Nop.Plugin.Payments.SimplePay.Models.Requests;
public enum ThreeDSReqAuthMethod
{
    [Description("01")]
    Guest = 1,
    [Description("02")]
    Registered = 2,
    [Description("05")]
    Registered3rdParty = 5
}
