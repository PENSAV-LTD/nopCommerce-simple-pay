using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers;
using Nop.Plugin.Payments.SimplePay.Processes;
using Nop.Plugin.Payments.SimplePay.Services;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Orders;
using Reqnroll.Microsoft.Extensions.DependencyInjection;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.Support;
internal class DependecyRegistrar
{
    public static Mock<HttpResponse> MockResponse { get; set; } = new Mock<HttpResponse>();
    public static Mock<HttpContext> MockContext { get; set; } = new Mock<HttpContext>();
    public static SimplePaySettings SimplePaySettings { get; set; } = new SimplePaySettings
    {
        MerchantKey = "merchantkey",
    };

    [ScenarioDependencies]
    public static IServiceCollection CreateServices()
    {
        var services = new ServiceCollection();
        IConfigurationRoot configuration = new ConfigurationBuilder().Build();

        SetupNop(services);
        new Nop.Plugin.Payments.SimplePay.Infrastructure.NopStartup().ConfigureServices(services, configuration);

        OrderProvider.Initialize();
        services.AddScoped<IOrderService>(sp =>
        {
            var mockOrderService = new Mock<IOrderService>();
            mockOrderService
            .Setup(x => x.GetOrderItemsAsync(It.IsAny<int>(), It.IsAny<bool?>(), It.IsAny<bool?>(), It.IsAny<int>()))
            .ReturnsAsync(OrderProvider.OrderItems);
            return mockOrderService.Object;
        });
        services.AddScoped<IProductService>(sp => {
            var mockProductService = new Mock<IProductService>();
            foreach (var product in OrderProvider.Products)
            {
                mockProductService
                    .Setup(x => x.GetProductByIdAsync(product.Id))
                    .ReturnsAsync(product);
            }
            return mockProductService.Object;
        });

        services.AddScoped<IOrderProcessingService>(sp => {
            var mockOrderProcessingService = new Mock<IOrderProcessingService>();
            mockOrderProcessingService
                .Setup(x => x.MarkOrderAsPaidAsync(It.IsAny<Order>()))
                .Callback(() => {
                    OrderProvider.Order.PaymentStatus = PaymentStatus.Paid;
                })
                .Returns(Task.CompletedTask);

            return mockOrderProcessingService.Object;
        });

        CustomerAndAddressProvider.Initialize();
        services.AddScoped<ICustomerService>(sp => {
            var mockCustomerService = new Mock<ICustomerService>();
            mockCustomerService
                .Setup(x => x.GetCustomerByIdAsync(CustomerAndAddressProvider.CustomerId))
                .ReturnsAsync(CustomerAndAddressProvider.Customer);
            mockCustomerService
                .Setup(x => x.GetCustomerByIdAsync(CustomerAndAddressProvider.CustomerWithoutBillingAddressId))
                .ReturnsAsync(CustomerAndAddressProvider.CustomerWithoutBillingAddress);

            return mockCustomerService.Object;
        });
        services.AddScoped<IAddressService>(sp => {
            var mockAddressService = new Mock<IAddressService>();
            mockAddressService
                .Setup(x => x.GetAddressByIdAsync(CustomerAndAddressProvider.BillingAddress.Id))
                .ReturnsAsync(CustomerAndAddressProvider.BillingAddress);
            return mockAddressService.Object;
        });
        services.AddScoped<ICountryService>(sp => {
            var mockCountryService = new Mock<ICountryService>();
            mockCountryService
                .Setup(x => x.GetCountryByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(CustomerAndAddressProvider.Country);
            return mockCountryService.Object;
        });
        services.AddScoped<IStateProvinceService>(sp => {
            var mockStateProvinceService = new Mock<IStateProvinceService>();
            mockStateProvinceService
                .Setup(x => x.GetStateProvinceByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(CustomerAndAddressProvider.StateProvince);
            return mockStateProvinceService.Object;
        });
        services.AddScoped<IResponseService>(sp => {
            var mockResponseService = new Mock<IResponseService>();
            return mockResponseService.Object;
        });

        services.AddScoped<IUrlHelperFactory>(sp => {
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper
                .Setup(x => x.Action(It.Is<UrlActionContext>(c =>
                    c.Action == "Success" &&
                    c.Controller == "SimplePayCallback"
                )))
                .Returns("http://localhost/simplepaycallback/success");
            mockUrlHelper
                .Setup(x => x.Action(It.Is<UrlActionContext>(c =>
                    c.Action == "Fail" &&
                    c.Controller == "SimplePayCallback"
                )))
                .Returns("http://localhost/simplepaycallback/fail");
            mockUrlHelper
                .Setup(x => x.Action(It.Is<UrlActionContext>(c =>
                    c.Action == "Cancel" &&
                    c.Controller == "SimplePayCallback"
                )))
                .Returns("http://localhost/simplepaycallback/cancel");
            mockUrlHelper
                .Setup(x => x.Action(It.Is<UrlActionContext>(c =>
                    c.Action == "Timeout" &&
                    c.Controller == "SimplePayCallback"
                )))
                .Returns("http://localhost/simplepaycallback/timeout");
            var mockUrlHelperFactory = new Mock<IUrlHelperFactory>();
            mockUrlHelperFactory.Setup(f => f.GetUrlHelper(It.IsAny<ControllerContext>())).Returns(mockUrlHelper.Object);
            return mockUrlHelperFactory.Object; 
        });
        services.AddScoped<IActionContextAccessor>(sp => {
            var actionContextAccessor = new Mock<IActionContextAccessor>();
            return actionContextAccessor.Object;
        });

        services.AddScoped<IHttpClientFactory, FakeHttpClientFactory>();
        services.AddScoped<SimplePaySettings>(sp => SimplePaySettings);
        services.AddScoped<HttpClientFactorySettings, HttpClientFactorySettings>();
        services.AddKeyedScoped<ISimplePayUrlsProvider, SimplePaySandboxUrls>("SANDBOX");
        services.AddKeyedScoped<ISimplePayUrlsProvider, SimplePayUrls>("PRODUCTION");
        services.AddScoped<SimplePayPaymentProcessor, SimplePayPaymentProcessor>();
        services.AddScoped<StartRequestDriver, StartRequestDriver>();

        MockContext.SetupGet(c => c.Response).Returns(MockResponse.Object);

        services.AddScoped<IHttpContextAccessor>(sp => {
            var mockAccessor = new Mock<IHttpContextAccessor>();
            mockAccessor.SetupGet(a => a.HttpContext).Returns(MockContext.Object);
            return mockAccessor.Object;
        });

        return services;
    }

    private static void SetupNop(IServiceCollection services)
    {
        var mockSettingService = new Mock<ISettingService>();
        services.AddSingleton<ISettingService>(mockSettingService.Object);
        var mockLocalizationService = new Mock<ILocalizationService>();
        services.AddSingleton<ILocalizationService>(mockLocalizationService.Object);
        var mockWebHelper = new Mock<IWebHelper>();
        services.AddSingleton<IWebHelper>(mockWebHelper.Object);
    }
}
