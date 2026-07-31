using Nop.Plugin.Payments.SimplePay.Domain;

namespace Nop.Plugin.Payments.SimplePay.Services;
public interface IResponseService
{
    public Task InsertResponseAsync(Responses response);

}
