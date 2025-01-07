using Nop.Core.Domain.Orders;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;
public static class OrderCreator
{
    public static Order Create(int id = 1, int customerId = 1)
    {
        return new Order
        {
            Id = id,
            OrderGuid = Guid.NewGuid(),
            CustomerId = customerId,
        };
    }
}
