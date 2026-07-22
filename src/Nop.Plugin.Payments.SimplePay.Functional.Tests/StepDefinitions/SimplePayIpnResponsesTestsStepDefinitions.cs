using System.Text.Json;
using Nop.Plugin.Payments.SimplePay.Controllers;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers;
using Nop.Plugin.Payments.SimplePay.Messages.Validators;
using Nop.Plugin.Payments.SimplePay.Models.Responses;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Services.Orders;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.StepDefinitions;

[Binding]
public class SimplePayIpnResponsesTestsStepDefinitions
{
    private readonly SimplePayIpnController _simplePayIpnController;
    private readonly IMessageToSendValidator _messageToSendValidator;

    public SimplePayIpnResponsesTestsStepDefinitions(
    IOrderService orderService,
    IOrderProcessingService orderProcessingService,
    IMessageToSendValidator messageToSendValidator,
    SimplePaySettings simplePaySettings
    )
    {
        _simplePayIpnController = new SimplePayIpnController(orderService, orderProcessingService, messageToSendValidator, simplePaySettings);
        _messageToSendValidator = messageToSendValidator;
    }

    [Then("Response's string contains ReceiveDate")]
    public void ThenResponsesStringContainsReceiveDate()
    {
        var jsonString = GetRequestJson();
        var resultJson = _simplePayIpnController.Ipn(jsonString).GetAwaiter().GetResult();
        var ipnResponse = JsonSerializer.Deserialize<IpnResponse>(resultJson);
        ipnResponse.Should().NotBeNull();
        ipnResponse!.ReceiveDate.Should().NotBe(default(DateTime));
        ipnResponse!.ReceiveDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Then("Response contains valid signature in HTTP header")]
    public void ThenResponseContainsValidSignatureInHTTPHeader()
    {
        var jsonString = GetRequestJson();
        _simplePayIpnController.Ipn(jsonString).GetAwaiter().GetResult();
        _simplePayIpnController.HttpContext.Response.Headers.Should().ContainKey("Signature");
    }

    private string GetRequestJson()
    {
        return $@"{{ ""status"":""FINISHED"", 
                    ""salt"":""223G0O18VAqdLhQYbJz73adT36YzLtak"", 
                    ""orderRef"":""{OrderProvider.Order.Id}"", 
                    ""method"":""CARD"", 
                    ""merchant"":""PUBLICTESTHUF"", 
                    ""finishDate"":""2019-09-09T14:46:18+02:00"", 
                    ""paymentDate"":""2019-09-09T14:41:13+02:00"", 
                    ""transactionId"":99844942 }}";
    }
}
