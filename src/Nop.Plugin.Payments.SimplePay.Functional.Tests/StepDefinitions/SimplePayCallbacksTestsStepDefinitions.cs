using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Payments.SimplePay.Controllers;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Support;
using Nop.Plugin.Payments.SimplePay.Messages.Validators;
using Nop.Plugin.Payments.SimplePay.Services;
using Nop.Plugin.Payments.SimplePay.Settings;
using Reqnroll;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.StepDefinitions;

[Binding]
public class SimplePayCallbacksTestsStepDefinitions
{
    private byte[] _responseJson;
    private string _signature;
    private readonly SimplePayCallbackController _callbackController;
    private readonly IMessageToSendValidator _messageToSendValidator;
    private ViewResult _viewResult;

    public SimplePayCallbacksTestsStepDefinitions(
        SimplePaySettings settings,
        IMessageToSendValidator messageToSendValidator,
        IResponseService responseService
        )
    {
        _callbackController = new SimplePayCallbackController(settings, messageToSendValidator, responseService);
        _messageToSendValidator = messageToSendValidator;
    }

    [Given("Callback setup for success response")]
    public void GivenCallbackSetupForSuccessResponse()
    {
        var jsonString = $@"{{""r"":0,""t"":99844942,""e"":""SUCCESS"",""m"":""{DependecyRegistrar.SimplePaySettings.MerchantKey}"",""o"":""101010515680292482600""}}";
        _responseJson = Encoding.UTF8.GetBytes(jsonString);
        _signature = _messageToSendValidator.CalculateSignature(DependecyRegistrar.SimplePaySettings.MerchantKey, jsonString);
    }

    [When("Callback is sent for success response")]
    public void WhenCallbackIsSentForSuccessResponse()
    {
        _viewResult = _callbackController.Success(Convert.ToBase64String(_responseJson), _signature).GetAwaiter().GetResult() as ViewResult;
    }

    [Then("Display success page")]
    public void ThenDisplaySuccessPage()
    {
        _viewResult.ViewName.Should().Be("~/Plugins/Payments.SimplePay/Views/Callback/Success.cshtml");
    }

    [Given("Callback setup for fail response")]
    public void GivenCallbackSetupForFailResponse()
    {
        throw new PendingStepException();
    }

    [When("Callback is sent for fail response")]
    public void WhenCallbackIsSentForFailResponse()
    {
        throw new PendingStepException();
    }

    [Then("Display failed page")]
    public void ThenDisplayFailedPage()
    {
        throw new PendingStepException();
    }

    [Given("Callback setup for timeout response")]
    public void GivenCallbackSetupForTimeoutResponse()
    {
        throw new PendingStepException();
    }

    [When("Callback is sent for timeout response")]
    public void WhenCallbackIsSentForTimeoutResponse()
    {
        throw new PendingStepException();
    }

    [Then("Display timeout page")]
    public void ThenDisplayTimeoutPage()
    {
        throw new PendingStepException();
    }

    [Given("Callback setup for cancel response")]
    public void GivenCallbackSetupForCancelResponse()
    {
        throw new PendingStepException();
    }

    [When("Callback is sent for cancel response")]
    public void WhenCallbackIsSentForCancelResponse()
    {
        throw new PendingStepException();
    }

    [Then("Display cancel page")]
    public void ThenDisplayCancelPage()
    {
        throw new PendingStepException();
    }

    [Given("Callback setup valid signature")]
    public void GivenCallbackSetupValidSignature()
    {
        throw new PendingStepException();
    }

    [When("Callback is sent for valid signature")]
    public void WhenCallbackIsSentForValidSignature()
    {
        throw new PendingStepException();
    }

    [Then("No exception is thrown for valid signature")]
    public void ThenNoExceptionIsThrownForValidSignature()
    {
        throw new PendingStepException();
    }

    [Given("Callback setup not valid signature")]
    public void GivenCallbackSetupNotValidSignature()
    {
        throw new PendingStepException();
    }

    [When("Callback is sent for not valid signature")]
    public void WhenCallbackIsSentForNotValidSignature()
    {
        throw new PendingStepException();
    }

    [Then("Throw exception is thrown for not valid signature")]
    public void ThenThrowExceptionIsThrownForNotValidSignature()
    {
        throw new PendingStepException();
    }

}
