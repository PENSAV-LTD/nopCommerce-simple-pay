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
        var paymentInfoViewModel = new ViewModels.PaymentInfoViewModel
        {
            CurrentStore = store
            //Order = additionalData as Nop.Core.Domain.Orders.Order,
            //PaymentUrl = additionalData is string url ? url : null
        };
        return View("~/Plugins/Payments.SimplePay/Views/Components/PaymentInfo.cshtml", paymentInfoViewModel);
    }
}
