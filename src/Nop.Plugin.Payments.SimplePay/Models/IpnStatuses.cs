namespace Nop.Plugin.Payments.SimplePay.Models;

public class IpnStatuses
{
    public const string FINISHED = "FINISHED";
    public const string AUTHORIZED = "AUTHORIZED";
    public const string REVERSED = "REVERSED";
    public const string CANCELED = "CANCELED";
    public const string TIMEOUT = "TIMEOUT";
}
