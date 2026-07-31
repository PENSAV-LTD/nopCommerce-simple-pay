using Nop.Core.Domain.Orders;
using Nop.Services.Payments;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;
public static class PostProcessPaymentRequestCreator
{
    public static PostProcessPaymentRequest Create(Order order)
    {
        order ??= new Order();
        return new PostProcessPaymentRequest
        {
            Order = order
        };
    }
}
