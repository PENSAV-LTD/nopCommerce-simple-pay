using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;
using Nop.Plugin.Payments.SimplePay.Processes;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Services.Catalog;
using Nop.Services.Orders;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Support;
internal class DependecyRegistrar
{
    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();
        IConfigurationRoot configuration = new ConfigurationBuilder().Build();

        //new Nop.Web.Framework.Infrastructure.NopStartup().ConfigureServices(services, configuration);

        //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BrandBankProductXmlObjectContext>());

        //TODO: add customizations, stubs required for testing
        //auto-reg all types from our assembly
        //builder.RegisterAssemblyTypes(typeof(TestDependencies).Assembly).SingleInstance();

        //auto-reg all [Binding] types from our assembly
        //builder.RegisterTypes(typeof(DependencyRegistrar).Assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(BindingAttribute))).ToArray()).SingleInstance();

        new Nop.Plugin.Payments.SimplePay.Infrastructure.NopStartup().ConfigureServices(services, configuration);

        var settings = new SimplePaySettings
        {
            MerchantKey = "merchantkey"
        };
        var mockOrderService = new Mock<IOrderService>();
        var mockProductService = new Mock<IProductService>();
        SetupOrderService(mockOrderService, mockProductService);
        services.AddSingleton(mockOrderService.Object);
        services.AddSingleton(mockProductService.Object);

        services.AddSingleton(settings);
        services.AddSingleton<HttpClientFactorySettings, HttpClientFactorySettings>();
        services.AddSingleton<IHttpClientFactory, FakeHttpClientFactory>();
        services.AddScoped<ISimplePayUrlsProvider, SimplePayTestUrls>();
        services.AddSingleton<SimplePayPaymentProcessor, SimplePayPaymentProcessor>();
        services.AddScoped<StartRequestDriver, StartRequestDriver>();

        return services;
    }

    private static void SetupOrderService(Mock<IOrderService> mockOrderService, Mock<IProductService> mockProductService)
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

        mockOrderService
            .Setup(x => x.GetOrderItemsAsync(It.IsAny<int>(), It.IsAny<bool?>(), It.IsAny<bool?>(), It.IsAny<int>()))
            .ReturnsAsync(new List<OrderItem> { orderItem1, orderItem2 });

        mockProductService
            .Setup(x => x.GetProductByIdAsync(id1))
            .ReturnsAsync(ProductCreator.Create(id1, "product1"));
        mockProductService
            .Setup(x => x.GetProductByIdAsync(id2))
            .ReturnsAsync(ProductCreator.Create(id2, "product2"));
    }
}
