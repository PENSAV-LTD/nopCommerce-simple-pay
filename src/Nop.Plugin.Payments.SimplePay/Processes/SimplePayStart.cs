using System.Text;
using System.Text.Json;
using Nop.Plugin.Payments.SimplePay.Messages.Validators;
using Nop.Plugin.Payments.SimplePay.Models.Requests;
using Nop.Plugin.Payments.SimplePay.Models.Responses;

namespace Nop.Plugin.Payments.SimplePay.Processes;
public class SimplePayStart
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISimplePayUrlsProvider _simplePayUrls;
    private readonly IMessageToSendValidator _messageToSendValidator;

    public SimplePayStart(
        IHttpClientFactory httpClientFactory,
        ISimplePayUrlsProvider simplePayUrls,
        IMessageToSendValidator messageToSendValidator
        )
    {
        _httpClientFactory = httpClientFactory;
        _simplePayUrls = simplePayUrls;
        _messageToSendValidator = messageToSendValidator;
    }

    public async Task<StartResponse> Send(StartRequest request)
    {
        string message = JsonSerializer.Serialize(request);
        using StringContent content = new(message, Encoding.UTF8, "application/json");
        content.Headers.Add("Signature", _messageToSendValidator.CalculateSignature(request.Merchant, message));

        var client = _httpClientFactory.CreateClient();
        using HttpResponseMessage response = await client.PostAsync(_simplePayUrls.StartUrl, content);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("SimplePay start request failed.");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<StartResponse>(responseContent);
    }
}
