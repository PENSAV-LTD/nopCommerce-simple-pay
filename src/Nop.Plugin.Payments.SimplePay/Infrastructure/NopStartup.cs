using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
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
        services.AddScoped<SimplePayStartRequest, SimplePayStartRequest>();
    }
}
