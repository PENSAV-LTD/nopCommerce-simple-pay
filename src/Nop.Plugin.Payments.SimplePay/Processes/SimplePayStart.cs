using System.Text;
using System.Text.Json;
using Nop.Plugin.Payments.SimplePay.Messages.Validators;
using Nop.Plugin.Payments.SimplePay.Models.Requests;
using Nop.Plugin.Payments.SimplePay.Models.Responses;
using Nop.Plugin.Payments.SimplePay.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Nop.Plugin.Payments.SimplePay.Processes;
public class SimplePayStart
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISimplePayUrlsProvider _sandboxUrlProvider;
    private readonly ISimplePayUrlsProvider _productionUrlProvider;
    private readonly IMessageToSendValidator _messageToSendValidator;
    private readonly SimplePaySettings _simplePaySettings;

    public SimplePayStart(
        IHttpClientFactory httpClientFactory,
        [FromKeyedServices("SANDBOX")] ISimplePayUrlsProvider sandboxUrlProvider,
        [FromKeyedServices("PRODUCTION")] ISimplePayUrlsProvider productionUrlProvider,
        IMessageToSendValidator messageToSendValidator,
        SimplePaySettings simplePaySettings
        )
    {
        _httpClientFactory = httpClientFactory;
        _sandboxUrlProvider = sandboxUrlProvider;
        _productionUrlProvider = productionUrlProvider;
        _messageToSendValidator = messageToSendValidator;
        _simplePaySettings = simplePaySettings;
    }

    public async Task<StartResponse> Send(StartRequest request)
    {
        var simplePayUrlProvider = GetSimplePayUrlProvider();
        string message = JsonSerializer.Serialize(request);
        using StringContent content = new(message, Encoding.UTF8, "application/json");
        content.Headers.Add("Signature", _messageToSendValidator.CalculateSignature(request.Merchant, message));

        var client = _httpClientFactory.CreateClient();
        using HttpResponseMessage response = await client.PostAsync(simplePayUrlProvider.StartUrl, content);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        response.Headers.TryGetValues("Signature", out var signatureValues);
        var responseSignature = _messageToSendValidator.CalculateSignature(
            request.Merchant,
            responseContent);
        if (signatureValues == null 
            || signatureValues.Count() == 0 
            || signatureValues.First() != responseSignature)
        {
            throw new InvalidOperationException("Response signature header is missing or invalid.");
        }
        return JsonSerializer.Deserialize<StartResponse>(responseContent);
    }

    private ISimplePayUrlsProvider GetSimplePayUrlProvider()
    {
        return _simplePaySettings.UseSandbox ? 
            _sandboxUrlProvider :
            _productionUrlProvider;
    }
}
