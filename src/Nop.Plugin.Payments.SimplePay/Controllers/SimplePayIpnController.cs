using System.Text;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Payments.SimplePay.Exceptions;
using Nop.Plugin.Payments.SimplePay.Messages.Validators;
using Nop.Plugin.Payments.SimplePay.Models;
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

    [HttpPost]
    public async Task<string> Ipn([FromBody] string param)
    {
        Request.EnableBuffering();
        using var reader = new StreamReader(Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        var jsonString = await reader.ReadToEndAsync();
        Request.Body.Position = 0;

        if (!ValidateSignature(jsonString))
        {
            throw new SimplePayInvalidSignatureException();
        }

        var request = JsonSerializer.Deserialize<IpnRequest>(jsonString);
        if (!int.TryParse(request.OrderRef, out var orderId))
        {
            throw new SimplePayInvalidOrderRefException();
        }
        if (request.Status == IpnStatuses.FINISHED)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order.PaymentStatus != Core.Domain.Payments.PaymentStatus.Paid)
            {
                await _orderProcessingService.MarkOrderAsPaidAsync(order);
            }
        }

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
