using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Nop.Plugin.Payments.SimplePay.Exceptions;
using Nop.Plugin.Payments.SimplePay.Messages.Validators;
using Nop.Plugin.Payments.SimplePay.Models.Requests;
using Nop.Plugin.Payments.SimplePay.Models.Responses;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Services.Orders;
using Nop.Web.Controllers;

namespace Nop.Plugin.Payments.SimplePay.Controllers;

public class SimplePayIpnController : BasePublicController
{
    private readonly IOrderService _orderService;
    private readonly IOrderProcessingService _orderProcessingService;
    private readonly IMessageToSendValidator _messageToSendValidator;
    private readonly SimplePaySettings _simplePaySettings;

    public SimplePayIpnController(
        IOrderService orderService,
        IOrderProcessingService orderProcessingService,
        IMessageToSendValidator messageToSendValidator,
        SimplePaySettings simplePaySettings
        )
    {
        _orderService = orderService;
        _orderProcessingService = orderProcessingService;
        _messageToSendValidator = messageToSendValidator;
        _simplePaySettings = simplePaySettings;
    }

    public async Task<string> Ipn(string jsonString)
    {
        if (!ValidateSignature(jsonString))
        {
            throw new SimplePayInvalidSignatureException();
        }

        var request = JsonSerializer.Deserialize<IpnRequest>(jsonString);
        if (!int.TryParse(request.OrderRef, out var orderId))
        {
            throw new SimplePayInvalidOrderRefException();
        }
        var order = await _orderService.GetOrderByIdAsync(orderId);
        await _orderProcessingService.MarkOrderAsPaidAsync(order);

        var response = SetResponseMessageAndHeader(request);
        return response;
    }

    private string SetResponseMessageAndHeader(IpnRequest message)
    {
        var response = new IpnResponse(message, DateTime.Now);
        var responseJson = JsonSerializer.Serialize(response);
        var calculateResponseSignature = _messageToSendValidator.CalculateSignature(_simplePaySettings.MerchantKey, responseJson);
        Response.Headers.Append("Signature", calculateResponseSignature);
        return responseJson;
    }

    private bool ValidateSignature(string message)
    {
        var signature = Request.Headers["Signature"];
        var calculatedSignatrue = _messageToSendValidator.CalculateSignature(_simplePaySettings.MerchantKey, message);
        return signature.Contains( calculatedSignatrue );
    }
}
