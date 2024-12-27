using Nop.Core.Domain.Orders;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;
public class OrderCreator
{
    public Order Creator()
    {
        return new Order
        {
            Id = 1,
            OrderGuid = Guid.NewGuid(),
            CustomerId = 1,
        };
    }
}
