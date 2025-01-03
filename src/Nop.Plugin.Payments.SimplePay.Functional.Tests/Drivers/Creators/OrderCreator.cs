using Nop.Core.Domain.Orders;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;
public static class OrderCreator
{
    public static Order Create()
    {
        return new Order
        {
            Id = 1,
            OrderGuid = Guid.NewGuid(),
            CustomerId = 1,
        };
    }
}
