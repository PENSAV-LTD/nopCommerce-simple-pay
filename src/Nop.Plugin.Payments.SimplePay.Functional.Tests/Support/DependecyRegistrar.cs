using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Moq;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers.Creators;
using Nop.Plugin.Payments.SimplePay.Models;
using Nop.Plugin.Payments.SimplePay.Processes;
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
    public static SimplePaySettings SimplePaySettings { get; set; } = new SimplePaySettings
    {
        MerchantKey = "merchantkey",
    };

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

        SetupNop(services);
        new Nop.Plugin.Payments.SimplePay.Infrastructure.NopStartup().ConfigureServices(services, configuration);

        var mockOrderService = new Mock<IOrderService>();
        var mockProductService = new Mock<IProductService>();
        SetupOrderService(mockOrderService, mockProductService);
        services.AddSingleton(mockOrderService.Object);
        services.AddSingleton(mockProductService.Object);

        var mockCustomerService = new Mock<ICustomerService>();
        var mockAddressService = new Mock<IAddressService>();
        var mockCountryService = new Mock<ICountryService>();
        var mockStateProvinceService = new Mock<IStateProvinceService>();
        SetupCustomerAndAddressServices(mockCustomerService, mockAddressService, mockCountryService, mockStateProvinceService);
        services.AddSingleton(mockCustomerService.Object);
        services.AddSingleton(mockAddressService.Object);
        services.AddSingleton(mockCountryService.Object);
        services.AddSingleton(mockStateProvinceService.Object);

        var mockUrlHelper = new Mock<IUrlHelper>();
        SetupUrlHelper(mockUrlHelper);

        services.AddSingleton(SimplePaySettings);
        services.AddSingleton<HttpClientFactorySettings, HttpClientFactorySettings>();
        services.AddSingleton<IHttpClientFactory, FakeHttpClientFactory>();
        services.AddSingleton<IUrlHelper>(mockUrlHelper.Object);
        services.AddKeyedScoped<ISimplePayUrlsProvider, SimplePaySandboxUrls>("SANDBOX");
        services.AddKeyedScoped<ISimplePayUrlsProvider, SimplePayUrls>("PRODUCTION");
        services.AddSingleton<SimplePayPaymentProcessor, SimplePayPaymentProcessor>();
        services.AddScoped<StartRequestDriver, StartRequestDriver>();

        return services;
    }

    private static void SetupCustomerAndAddressServices(Mock<ICustomerService> mockCustomerService, Mock<IAddressService> mockAddressService, Mock<ICountryService> mockCountryService, Mock<IStateProvinceService> mockStateProvinceService)
    {
        CustomerAndAddressProvider.Initialize();

        mockCustomerService
            .Setup(x => x.GetCustomerByIdAsync(CustomerAndAddressProvider.CustomerId))
            .ReturnsAsync(CustomerAndAddressProvider.Customer);
        mockCustomerService
            .Setup(x => x.GetCustomerByIdAsync(CustomerAndAddressProvider.CustomerWithoutBillingAddressId))
            .ReturnsAsync(CustomerAndAddressProvider.CustomerWithoutBillingAddress);
        mockAddressService
            .Setup(x => x.GetAddressByIdAsync(CustomerAndAddressProvider.BillingAddress.Id))
            .ReturnsAsync(CustomerAndAddressProvider.BillingAddress);
        mockCountryService
            .Setup(x => x.GetCountryByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(CustomerAndAddressProvider.Country);
        mockStateProvinceService
            .Setup(x => x.GetStateProvinceByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(CustomerAndAddressProvider.StateProvince);
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

    private static void SetupOrderService(Mock<IOrderService> mockOrderService, Mock<IProductService> mockProductService)
    {
        OrderProvider.Initialize();

        mockOrderService
            .Setup(x => x.GetOrderItemsAsync(It.IsAny<int>(), It.IsAny<bool?>(), It.IsAny<bool?>(), It.IsAny<int>()))
            .ReturnsAsync(OrderProvider.OrderItems);

        foreach(var product in OrderProvider.Products)
        {
            mockProductService
                .Setup(x => x.GetProductByIdAsync(product.Id))
                .ReturnsAsync(product);
        }
    }

    private static void SetupUrlHelper(Mock<IUrlHelper> mockUrlHelper)
    {
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
    }
}
