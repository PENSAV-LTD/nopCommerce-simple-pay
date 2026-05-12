using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
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

        var message = JsonSerializer.Deserialize<IpnRequest>(jsonString);
        if (!int.TryParse(message.OrderRef, out var orderId))
        {
            throw new SimplePayInvalidOrderRefException();
        }
        var order = await _orderService.GetOrderByIdAsync(orderId);
        await _orderProcessingService.MarkOrderAsPaidAsync(order);
        return "OK";
    }

    private bool ValidateSignature(string message)
    {
        var signature = Request.Headers["Signature"];
        var calculatedSignatrue = _messageToSendValidator.CalculateSignature(_simplePaySettings.MerchantKey, message);
        return signature.Contains( calculatedSignatrue );
    }
}
