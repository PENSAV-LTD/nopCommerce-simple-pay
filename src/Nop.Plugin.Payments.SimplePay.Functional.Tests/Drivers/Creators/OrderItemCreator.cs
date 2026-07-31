using Nop.Core.Domain.Orders;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;
public static class OrderItemCreator
{
    public static OrderItem Create(
        int? productId = null,
        int? unitPrice = null,
        int? quantity = null,
        int? taxRate = null
        )
    {
        productId ??= 1;
        unitPrice ??= 100;
        quantity ??= 1;
        taxRate ??= 27;
        return new OrderItem
        {
            ProductId = productId.Value,
            UnitPriceExclTax = unitPrice.Value,
            UnitPriceInclTax = unitPrice.Value * (1 + taxRate.Value / 100),
            Quantity = quantity.Value,
            PriceExclTax = unitPrice.Value * quantity.Value,
            PriceInclTax = unitPrice.Value * (1 + taxRate.Value / 100) * quantity.Value
        };
    }
}
