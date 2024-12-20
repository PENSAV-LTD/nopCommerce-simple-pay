using Microsoft.AspNetCore.Http;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.SimplePay.Components;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Payments;

namespace Nop.Plugin.Payments.SimplePay;
public class SimplePayPaymentProcessor : SimplePayPaymentModule, IPaymentMethod
{
    public bool SupportCapture => true;

    public bool SupportPartiallyRefund => true;

    public bool SupportRefund => true;

    public bool SupportVoid => true;

    public RecurringPaymentType RecurringPaymentType => RecurringPaymentType.NotSupported;

    public PaymentMethodType PaymentMethodType => PaymentMethodType.Redirection;

    public bool SkipPaymentInfo => false;

    public Task<CancelRecurringPaymentResult> CancelRecurringPaymentAsync(CancelRecurringPaymentRequest cancelPaymentRequest)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CanRePostProcessPaymentAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<CapturePaymentResult> CaptureAsync(CapturePaymentRequest capturePaymentRequest)
    {
        // finish call
        throw new NotImplementedException();
    }

    public Task<decimal> GetAdditionalHandlingFeeAsync(IList<ShoppingCartItem> cart)
    {
        return Task.FromResult(_simplePaySettings.AdditionalFee);
    }

    public Task<ProcessPaymentRequest> GetPaymentInfoAsync(IFormCollection form)
    {
        return Task.FromResult(new ProcessPaymentRequest());
    }

    public async Task<string> GetPaymentMethodDescriptionAsync()
    {
        return await _localizationService.GetResourceAsync("Plugins.Payment.CheckMoneyOrderPaymentMethodDescription");
    }

    public Type GetPublicViewComponent()
    {
        return typeof(PaymentInfoViewComponent);
    }

    public Task<bool> HidePaymentMethodAsync(IList<ShoppingCartItem> cart)
    {
        return Task.FromResult(false);
    }

    public Task PostProcessPaymentAsync(PostProcessPaymentRequest postProcessPaymentRequest)
    {
        // start call
        throw new NotImplementedException();
    }

    public Task<ProcessPaymentResult> ProcessPaymentAsync(ProcessPaymentRequest processPaymentRequest)
    {
        return Task.FromResult(new ProcessPaymentResult());
    }

    public Task<ProcessPaymentResult> ProcessRecurringPaymentAsync(ProcessPaymentRequest processPaymentRequest)
    {
        throw new NotImplementedException();
    }

    public Task<RefundPaymentResult> RefundAsync(RefundPaymentRequest refundPaymentRequest)
    {
        // refund call
        throw new NotImplementedException();
    }

    public Task<IList<string>> ValidatePaymentFormAsync(IFormCollection form)
    {
        return Task.FromResult<IList<string>>(new List<string>());
    }

    public Task<VoidPaymentResult> VoidAsync(VoidPaymentRequest voidPaymentRequest)
    {
        // finish with 0 total
        throw new NotImplementedException();
    }

    private readonly SimplePaySettings _simplePaySettings;

    public SimplePayPaymentProcessor(
        SimplePaySettings simplePaySettings,
        ISettingService settingService, 
        ILocalizationService localizationService,
        IWebHelper webHelper) 
        : base(settingService, localizationService, webHelper)
    {
        _simplePaySettings = simplePaySettings;
    }
}
