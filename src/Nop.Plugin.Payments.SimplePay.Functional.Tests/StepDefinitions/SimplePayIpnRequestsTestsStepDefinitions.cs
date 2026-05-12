using System;
using Microsoft.AspNetCore.Http;
using Nop.Plugin.Payments.SimplePay.Controllers;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Support;
using Nop.Plugin.Payments.SimplePay.Messages.Validators;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Services.Orders;
using Reqnroll;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.StepDefinitions;

[Binding]
public class SimplePayIpnRequestsTestsStepDefinitions
{
    private readonly SimplePayIpnController _simplePayIpnController;
    private readonly IMessageToSendValidator _messageToSendValidator;

    private string _jsonString;
    private string _signature;

    public SimplePayIpnRequestsTestsStepDefinitions(
        IOrderService orderService,
        IOrderProcessingService orderProcessingService,
        IMessageToSendValidator messageToSendValidator,
        SimplePaySettings simplePaySettings
        )
    {
        _simplePayIpnController = new SimplePayIpnController(orderService, orderProcessingService, messageToSendValidator, simplePaySettings);
        _messageToSendValidator = messageToSendValidator;
    }

    [Given("IpnRequest setup for Validate")]
    public void GivenIpnRequestSetupForValidate()
    {
        SetupSignatureAndJsonWithStatus("FINISHED");
    }

    [When("IpnRequest is sent for Validate")]
    public void WhenIpnRequestIsSentForValidate()
    {
        SetupHttpContextWithSignatureHeader(_signature);
    }

    [Then("Response is Validate")]
    public void ThenResponseIsValidate()
    {
        _simplePayIpnController.Ipn(_jsonString).GetAwaiter().GetResult().Should().Be("OK");
    }

    [Given("IpnRequest setup for Finished")]
    public void GivenIpnRequestSetupForFinished()
    {
        SetupSignatureAndJsonWithStatus("FINISHED");
    }

    [When("IpnRequest is sent for Finished")]
    public void WhenIpnRequestIsSentForFinished()
    {
        SetupHttpContextWithSignatureHeader(_signature);
    }

    [Then("Response is Finished")]
    public void ThenResponseIsFinished()
    {
        _simplePayIpnController.Ipn(_jsonString).GetAwaiter().GetResult().Should().Be("OK");
    }

    [Then("Order set as payed")]
    public void ThenOrderSetAsPayed()
    {
        Assert.Equal((int)Core.Domain.Payments.PaymentStatus.Paid, OrderProvider.Order.PaymentStatusId);
    }

    [Given("IpnRequest setup for Authorized")]
    public void GivenIpnRequestSetupForAuthorized()
    {
        throw new PendingStepException();
    }

    [When("IpnRequest is sent for Authorized")]
    public void WhenIpnRequestIsSentForAuthorized()
    {
        throw new PendingStepException();
    }

    [Then("Response is Authorized")]
    public void ThenResponseIsAuthorized()
    {
        throw new PendingStepException();
    }

    [Given("IpnRequest setup for Reversed")]
    public void GivenIpnRequestSetupForReversed()
    {
        throw new PendingStepException();
    }

    [When("IpnRequest is sent for Reversed")]
    public void WhenIpnRequestIsSentForReversed()
    {
        throw new PendingStepException();
    }

    [Then("Response is Reversed")]
    public void ThenResponseIsReversed()
    {
        throw new PendingStepException();
    }

    [Given("IpnRequest setup for Cancelled")]
    public void GivenIpnRequestSetupForCancelled()
    {
        throw new PendingStepException();
    }

    [When("IpnRequest is sent for Cancelled")]
    public void WhenIpnRequestIsSentForCancelled()
    {
        throw new PendingStepException();
    }

    [Then("Response is Cancelled")]
    public void ThenResponseIsCancelled()
    {
        throw new PendingStepException();
    }

    [Given("IpnRequest setup for Timeout")]
    public void GivenIpnRequestSetupForTimeout()
    {
        throw new PendingStepException();
    }

    [When("IpnRequest is sent for Timeout")]
    public void WhenIpnRequestIsSentForTimeout()
    {
        throw new PendingStepException();
    }

    [Then("Response is Timeout")]
    public void ThenResponseIsTimeout()
    {
        throw new PendingStepException();
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
