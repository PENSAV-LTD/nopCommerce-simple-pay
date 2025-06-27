using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers;
public static class OrderProvider
{
    public static int OrderId = 1;
    public static int OrderWithoutBillingAddressId = 2;
    public static Order Order { get; set; }
    public static Order OrderWithoutBillingAddress { get; set; }
    public static IList<OrderItem> OrderItems { get; set; }
    public static IList<Product> Products { get; set; }
    public static string CustomerCurrencyCode { get; set; } = "EUR";

    public static void Initialize()
    {
        var id1 = 1;
        var orderItem1 = OrderItemCreator.Create(
            productId: id1,
            quantity: 2,
            unitPrice: 25,
            taxRate: 27
            );

        var id2 = 2;
        var orderItem2 = OrderItemCreator.Create(
            productId: id2,
            quantity: 1,
            unitPrice: 40,
            taxRate: 27
            );


        OrderItems = new List<OrderItem> { orderItem1, orderItem2 };
        Products = new List<Product> {
            ProductCreator.Create(id1, "product1"),
            ProductCreator.Create(id2, "product2")
        };

        Order = OrderCreator.Create(OrderId, CustomerAndAddressProvider.CustomerId);
        Order.OrderTotal = OrderItems.Sum(x => x.PriceInclTax);
        Order.OrderTax = OrderItems.Sum(x => x.PriceInclTax) - OrderItems.Sum(x => x.PriceExclTax);
        Order.CustomerCurrencyCode = CustomerCurrencyCode;

        OrderWithoutBillingAddress = OrderCreator.Create(OrderWithoutBillingAddressId, CustomerAndAddressProvider.CustomerWithoutBillingAddressId);
        OrderWithoutBillingAddress.OrderTotal = OrderItems.Sum(x => x.PriceInclTax);
        OrderWithoutBillingAddress.OrderTax = OrderItems.Sum(x => x.PriceInclTax) - OrderItems.Sum(x => x.PriceExclTax);
    }
}
