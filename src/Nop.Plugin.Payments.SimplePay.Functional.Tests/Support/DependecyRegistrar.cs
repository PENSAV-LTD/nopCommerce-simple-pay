using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Plugin.Payments.SimplePay.Settings;
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

        services.AddSingleton(settings);

        return services;
    }
}
