using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Stores;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Payments.SimplePay.Components;
[ViewComponent(Name = SIMPLE_PAY_PAYMENT_INFO_VIEW_COMPONENT_NAME)]

public class PaymentInfoViewComponent : NopViewComponent
{
    public const string SIMPLE_PAY_PAYMENT_INFO_VIEW_COMPONENT_NAME = "SimplePayPaymentInfoView";
    private readonly IStoreContext _storeContext;

    public PaymentInfoViewComponent(
        IStoreContext storeContext
        )
    {
        _storeContext = storeContext;
    }

    public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
    {
        var store = await _storeContext.GetCurrentStoreAsync();
        return View("~/Plugins/Payments.SimpleyPay/Views/Components/PaymentInfo.cshtml");
    }
}
