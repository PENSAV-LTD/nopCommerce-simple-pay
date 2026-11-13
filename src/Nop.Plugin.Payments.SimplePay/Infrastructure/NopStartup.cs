using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Payments.SimplePay.Messages.Generators;
using Nop.Plugin.Payments.SimplePay.Messages.Validators;
using Nop.Plugin.Payments.SimplePay.Processes;
using Nop.Plugin.Payments.SimplePay.Services;
using Nop.Plugin.Payments.SimplePay.Transactions;

namespace Nop.Plugin.Payments.SimplePay.Infrastructure;
internal class NopStartup : INopStartup
{
    public int Order => 99;

    public void Configure(IApplicationBuilder application)
    {
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMessageToSendValidator, MessageToSendValidator>();
        services.AddScoped<IResponseService, ResponseService>();

        services.AddScoped<SimplePayStartRequest, SimplePayStartRequest>();
        services.AddScoped<SimplePayStart, SimplePayStart>();
        services.AddScoped<ISaltGenerator, SaltGenerator>();
        services.AddKeyedScoped<ISimplePayUrlsProvider, SimplePaySandboxUrls>("SANDBOX");
        services.AddKeyedScoped<ISimplePayUrlsProvider, SimplePayUrls>("PRODUCTION");
    }
}
