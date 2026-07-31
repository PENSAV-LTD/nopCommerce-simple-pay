using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc.Routing;
using Nop.Web.Infrastructure;

namespace Nop.Plugin.Payments.SimplePay.Infrastructure;
public class RouteProvider : BaseRouteProvider, IRouteProvider
{
    public int Priority => 0;

    public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapControllerRoute(name: "SimplePayControllerSuccess",
            pattern: "SimplePayCallback/Success",
            defaults: new { controller = "SimplePayCallback", action = "Success" });
        endpointRouteBuilder.MapControllerRoute(name: "SimplePayControllerFail",
            pattern: "SimplePayCallback/Fail",
            defaults: new { controller = "SimplePayCallback", action = "Fail" });
        endpointRouteBuilder.MapControllerRoute(name: "SimplePayControllerCancel",
            pattern: "SimplePayCallback/Cancel",
            defaults: new { controller = "SimplePayCallback", action = "Cancel" });
        endpointRouteBuilder.MapControllerRoute(name: "SimplePayControllerTimeout",
            pattern: "SimplePayCallback/Timeout",
            defaults: new { controller = "SimplePayCallback", action = "Timeout" });
    }
}
