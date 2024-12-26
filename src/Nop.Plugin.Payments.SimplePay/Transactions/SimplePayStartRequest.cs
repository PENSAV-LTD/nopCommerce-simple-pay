using Nop.Plugin.Payments.SimplePay.Models.Requests;
using Nop.Plugin.Payments.SimplePay.Settings;

namespace Nop.Plugin.Payments.SimplePay.Transactions;
public class SimplePayStartRequest
{
    private readonly SimplePaySettings _settings;

    public SimplePayStartRequest(
        SimplePaySettings settings
        )
    {
        _settings = settings;
    }
    public StartRequest CreateStartRequest()
    {
        return new StartRequest
        {
            Merchant = _settings.MerchantKey
        };
    }
}
