using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Nop.Plugin.Payments.SimplePay.Controllers;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Support;
using Nop.Plugin.Payments.SimplePay.Messages.Validators;
using Nop.Plugin.Payments.SimplePay.Models;
using Nop.Plugin.Payments.SimplePay.Models.Responses;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Services.Orders;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.StepDefinitions;

[Binding]
public class SimplePayIpnResponsesTestsStepDefinitions
{
    private readonly SimplePayIpnController _simplePayIpnController;
    private readonly IMessageToSendValidator _messageToSendValidator;
    private string _signature;
    private string _jsonString;
    private string _resultJson;

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

    [Given("Setup IpnRequest")]
    public void GivenSetupIpnRequest()
    {
        SetupSignatureAndJsonWithStatus(IpnStatuses.FINISHED);
        SetupHttpContextWithSignatureHeader(_signature);
        _resultJson = _simplePayIpnController.Ipn(_jsonString).GetAwaiter().GetResult();
    }


    [Then("Response's string contains ReceiveDate")]
    public void ThenResponsesStringContainsReceiveDate()
    {
        var ipnResponse = JsonSerializer.Deserialize<IpnResponse>(_resultJson);
        ipnResponse.Should().NotBeNull();
        ipnResponse!.ReceiveDate.Should().NotBe(default(DateTime));
        ipnResponse!.ReceiveDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(5));
    }

    [Then("Response contains valid signature in HTTP header")]
    public void ThenResponseContainsValidSignatureInHTTPHeader()
    {
        _simplePayIpnController.HttpContext.Response.Headers.Should().ContainKey("Signature");
    }

    private void SetupSignatureAndJsonWithStatus(string status)
    {
        _jsonString = $@"{{ ""status"":""{status}"", 
                            ""salt"":""223G0O18VAqdLhQYbJz73adT36YzLtak"", 
                            ""orderRef"":""{OrderProvider.Order.Id}"", 
                            ""method"":""CARD"", 
                            ""merchant"":""PUBLICTESTHUF"", 
                            ""finishDate"":""2019-09-09T14:46:18+02:00"", 
                            ""paymentDate"":""2019-09-09T14:41:13+02:00"", 
                            ""transactionId"":99844942 }}";
        _signature = _messageToSendValidator.CalculateSignature(DependecyRegistrar.SimplePaySettings.MerchantKey, _jsonString);
    }

    private void SetupHttpContextWithSignatureHeader(string signature)
    {
        _simplePayIpnController.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext()
        {
            HttpContext = new DefaultHttpContext()
            {
                Request =
                    {
                        Headers =
                        {
                            new KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>("Signature", signature)
                        }
                    }
            }
        };
    }

}
