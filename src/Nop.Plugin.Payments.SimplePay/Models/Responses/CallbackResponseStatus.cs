using System.ComponentModel;

namespace Nop.Plugin.Payments.SimplePay.Models.Responses;
public enum CallbackResponseStatus
{
    [Description("SUCCESS")]
    Success = 1,
    [Description("FAIL")]
    Fail = 2,
    [Description("TIMEOUT")]
    TimeOut = 3,
    [Description("CANCEL")]
    Cancel = 5,

}
