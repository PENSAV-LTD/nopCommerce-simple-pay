using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Payments.SimplePay.ViewModels;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Payments.SimplePay.Controllers;

[AutoValidateAntiforgeryToken]
[AuthorizeAdmin]
[Area(AreaNames.ADMIN)]
public class SimplePayConfigurationController : BasePaymentController
{
    public void Configure()
    {
    }

    [HttpPost]
    public void Configure(PaymentConfiguration configuration) 
    {
    }
}
