using System.Text;
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
        var response = await ProcessResponse(r, s);
        if (response.Event != "SUCCESS")
            throw new InvalidOperationException("Invalid event type for success callback: " + response.Event);
        return View("~/Plugins/Payments.SimplePay/Views/Callback/Success.cshtml");
    }

    public async Task<ViewResult> Fail(string r, string s)
    {
        var response = await ProcessResponse(r, s);
        if (response.Event != "FAIL")
            throw new InvalidOperationException("Invalid event type for fail callback: " + response.Event);
        return View("~/Plugins/Payments.SimplePay/Views/Callback/Fail.cshtml");
    }

    public async Task<ViewResult> Cancel(string r, string s)
    {
        var response = await ProcessResponse(r, s);
        if (response.Event != "CANCEL")
            throw new InvalidOperationException("Invalid event type for cancel callback: " + response.Event);
        return View("~/Plugins/Payments.SimplePay/Views/Callback/Cancel.cshtml");
    }

    public async Task<ViewResult> Timeout(string r, string s)
    {
        var response = await ProcessResponse(r, s);
        if (response.Event != "TIMEOUT")
            throw new InvalidOperationException("Invalid event type for timeout callback: " + response.Event);
        return View("~/Plugins/Payments.SimplePay/Views/Callback/Timeout.cshtml");
    }

    private async Task<CallbackResponse> ProcessResponse(string jsonString, string signature)
    {
        var response = ValidateAndGetResponse(jsonString, signature);
        await _responseService.InsertResponseAsync(ConvertToDomain(response));
        return response;
    }

    private CallbackResponse ValidateAndGetResponse(string jsonStr, string signature)
    {
        var merchantKey = _simplePaySettings.MerchantKey;
        var decodedJsonStr = Convert.FromBase64String(jsonStr);
        var json = Encoding.UTF8.GetString(decodedJsonStr);
        if (_messageToSendValidator.CalculateSignature(merchantKey, json) != signature)
        {
            throw new SimplePayException("SimplePayCallback: Invalid signature");
        }
        return JsonSerializer.Deserialize<CallbackResponse>(json);
    }

    private Responses ConvertToDomain(CallbackResponse callbackResponse)
    {
        if (callbackResponse == null)
            throw new ArgumentNullException(nameof(callbackResponse));
        return new Responses
        {
            OrderId = callbackResponse.OrderRef,
            Code = callbackResponse.ResponseCode,
            MerchantId = callbackResponse.Merchant,
            TransactionId = callbackResponse.TransactionId,
            Events = new ResponseEvents { Name = callbackResponse.Event }
        };
    }
}
