using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Nop.Core;
using Nop.Core.Domain.Orders;
using Nop.Plugin.Payments.SimplePay.Components;
using Nop.Plugin.Payments.SimplePay.Models.Requests;
using Nop.Plugin.Payments.SimplePay.Processes;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Plugin.Payments.SimplePay.Transactions;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Payments;
using Nop.Services.Plugins;

namespace Nop.Plugin.Payments.SimplePay;
public class SimplePayPaymentProcessor : BasePlugin, IPaymentMethod
{
    public bool SupportCapture => false;

    public bool SupportPartiallyRefund => false;

    public bool SupportRefund => false;

    public bool SupportVoid => false;

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

    public async Task PostProcessPaymentAsync(PostProcessPaymentRequest postProcessPaymentRequest)
    {
        var startRequest = await _simplePayStartRequest.CreateStartRequestAsync(postProcessPaymentRequest.Order);
        var startResponse = await _simplePayStart.Send(startRequest);
        if (string.IsNullOrEmpty(startResponse.PaymentUrl))
            throw new NopException("Payment URL is empty");
        var url = QueryHelpers.
            AddQueryString(
            $"{_webHelper.GetStoreLocation()}/SimplePay/Payment",
            new List<KeyValuePair<string, string>>
            {
                new("paymentUrl", startResponse.PaymentUrl),
                new("orderId", postProcessPaymentRequest.Order.Id.ToString())
            });
        _httpContextAccessor.HttpContext.Response.Redirect(url);
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

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/SimplePayConfiguration/Configure";
    }

    public override async Task InstallAsync()
    {
        await _settingService.SaveSettingAsync(new SimplePaySettings());

        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
        });
        await base.InstallAsync();
    }

    private readonly SimplePaySettings _simplePaySettings;
    private readonly SimplePayStart _simplePayStart;
    private readonly SimplePayStartRequest _simplePayStartRequest;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISettingService _settingService;
    protected readonly ILocalizationService _localizationService;
    protected readonly IWebHelper _webHelper;

    public SimplePayPaymentProcessor(
        SimplePaySettings simplePaySettings,
        SimplePayStart simplePayStart,
        SimplePayStartRequest simplePayStartRequest,
        IHttpContextAccessor httpContextAccessor,
        ISettingService settingService, 
        ILocalizationService localizationService,
        IWebHelper webHelper) 
    {
        _simplePaySettings = simplePaySettings;
        _simplePayStart = simplePayStart;
        _simplePayStartRequest = simplePayStartRequest;
        _httpContextAccessor = httpContextAccessor;
        _settingService = settingService;
        _localizationService = localizationService;
        _webHelper = webHelper;
    }
}
