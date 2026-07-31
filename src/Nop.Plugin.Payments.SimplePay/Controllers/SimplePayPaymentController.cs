using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Orders;
using Nop.Web.Controllers;
using Nop.Web.Framework;

namespace Nop.Plugin.Payments.SimplePay.Controllers;
[Area(AreaNames.ADMIN)]
public class SimplePayPaymentController : BasePublicController
{
    private readonly IOrderService _orderService;
    private readonly IStoreContext _storeContext;

    public SimplePayPaymentController(
        IOrderService orderService,
        IStoreContext storeContext
        )
    {
        _orderService = orderService;
        _storeContext = storeContext;
    }

    public async Task<ViewResult> Payment(string paymentUrl, int orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        if (order == null)
            throw new ArgumentException("No order found with the specified id");
        var model = new ViewModels.PaymentViewModel
        {
            Order = order,
            PaymentUrl = paymentUrl,
            CurrentStore = await _storeContext.GetCurrentStoreAsync()
        };
        return View("~/Plugins/Payments.SimplePay/Views/Payment/Payment.cshtml", model);
    }
}
