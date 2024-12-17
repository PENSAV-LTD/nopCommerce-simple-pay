namespace Nop.Plugin.Payments.SimplePay.Models.Responses;
internal class IpnStatus
{
    public static readonly string FINISHED = "FINISHED";
    public static readonly string AUTHORIZED = "AUTHORIZED";
    public static readonly string REVERSED = "REVERSED";
    public static readonly string CANCELLED = "CANCELLED";
    public static readonly string TIMEOUT = "TIMEOUT";
}
