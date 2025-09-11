using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Payments.SimplePay.Domain;
using Nop.Plugin.Payments.SimplePay.Exceptions;
using Nop.Plugin.Payments.SimplePay.Messages.Validators;
using Nop.Plugin.Payments.SimplePay.Models.Responses;
using Nop.Plugin.Payments.SimplePay.Services;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Web.Controllers;

namespace Nop.Plugin.Payments.SimplePay.Controllers;
public class SimplePayCallbackController : BasePublicController
{
    readonly IMessageToSendValidator _messageToSendValidator;
    private readonly IResponseService _responseService;
    readonly SimplePaySettings _simplePaySettings;

    public SimplePayCallbackController(
        SimplePaySettings simplePaySettings,
        IMessageToSendValidator messageToSendValidator,
        IResponseService responseService
        )
    {
        _simplePaySettings = simplePaySettings;
        _messageToSendValidator = messageToSendValidator;
        _responseService = responseService;
    }

    public async Task<ViewResult> Success(string r, string s)
    {
        await ProcessResponse(r, s);
        return View("~/Plugins/Payments.SimpleyPay/Views/Callback/Success.cshtml");
    }

    public async Task<ViewResult> Fail(string r, string s)
    {
        await ProcessResponse(r, s);
        return View("~/Plugins/Payments.SimpleyPay/Views/Callback/Fail.cshtml");
    }

    public async Task<ViewResult> Cancel(string r, string s)
    {
        await ProcessResponse(r, s);
        return View("~/Plugins/Payments.SimpleyPay/Views/Callback/Cancel.cshtml");
    }

    public async Task<ViewResult> Timeout(string r, string s)
    {
        await ProcessResponse(r, s);
        return View("~/Plugins/Payments.SimpleyPay/Views/Callback/Timeout.cshtml");
    }

    private async Task ProcessResponse(string jsonString, string signature)
    {
        var response = ValidateAndGetResponse(jsonString, signature);
        await _responseService.InsertResponseAsync(ConvertToDomain(response));
    }

    private CallbackResponse ValidateAndGetResponse(string jsonStr, string signature)
    {
        var merchantKey = _simplePaySettings.MerchantKey;
        if (_messageToSendValidator.CalculateSignature(merchantKey, jsonStr) != signature)
        {
            throw new SimplePayException("SimplePayCallback: Invalid signature");
        }
        return JsonSerializer.Deserialize<CallbackResponse>(jsonStr);
    }

    private Responses ConvertToDomain(CallbackResponse callbackResponse)
    {
        if (callbackResponse == null)
            throw new ArgumentNullException(nameof(callbackResponse));
        return new Responses
        {
            OrderId = callbackResponse.OrderRef,
            Code = int.Parse(callbackResponse.ResponseCode),
            MerchantId = callbackResponse.Merchant,
            TransactionId = callbackResponse.TransactionId,
            Events = new ResponseEvents { Name = callbackResponse.Event }
        };
    }
}
