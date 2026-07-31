using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Payments.SimplePay.Controllers;
using Nop.Plugin.Payments.SimplePay.Exceptions;
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
        var jsonString = $@"{{""r"":1,""t"":99844942,""e"":""FAIL"",""m"":""{DependecyRegistrar.SimplePaySettings.MerchantKey}"",""o"":""101010515680292482600""}}";
        _responseJson = Encoding.UTF8.GetBytes(jsonString);
        _signature = _messageToSendValidator.CalculateSignature(DependecyRegistrar.SimplePaySettings.MerchantKey, jsonString);
    }

    [When("Callback is sent for fail response")]
    public void WhenCallbackIsSentForFailResponse()
    {
        _viewResult = _callbackController.Fail(Convert.ToBase64String(_responseJson), _signature).GetAwaiter().GetResult() as ViewResult;
    }

    [Then("Display failed page")]
    public void ThenDisplayFailedPage()
    {
        _viewResult.ViewName.Should().Be("~/Plugins/Payments.SimplePay/Views/Callback/Fail.cshtml");
    }

    [Given("Callback setup for timeout response")]
    public void GivenCallbackSetupForTimeoutResponse()
    {
        var jsonString = $@"{{""r"":2,""t"":99844942,""e"":""TIMEOUT"",""m"":""{DependecyRegistrar.SimplePaySettings.MerchantKey}"",""o"":""101010515680292482600""}}";
        _responseJson = Encoding.UTF8.GetBytes(jsonString);
        _signature = _messageToSendValidator.CalculateSignature(DependecyRegistrar.SimplePaySettings.MerchantKey, jsonString);
    }

    [When("Callback is sent for timeout response")]
    public void WhenCallbackIsSentForTimeoutResponse()
    {
        _viewResult = _callbackController.Timeout(Convert.ToBase64String(_responseJson), _signature).GetAwaiter().GetResult() as ViewResult;
    }

    [Then("Display timeout page")]
    public void ThenDisplayTimeoutPage()
    {
        _viewResult.ViewName.Should().Be("~/Plugins/Payments.SimplePay/Views/Callback/Timeout.cshtml");
    }

    [Given("Callback setup for cancel response")]
    public void GivenCallbackSetupForCancelResponse()
    {
        var jsonString = $@"{{""r"":3,""t"":99844942,""e"":""CANCEL"",""m"":""{DependecyRegistrar.SimplePaySettings.MerchantKey}"",""o"":""101010515680292482600""}}";
        _responseJson = Encoding.UTF8.GetBytes(jsonString);
        _signature = _messageToSendValidator.CalculateSignature(DependecyRegistrar.SimplePaySettings.MerchantKey, jsonString);
    }

    [When("Callback is sent for cancel response")]
    public void WhenCallbackIsSentForCancelResponse()
    {
        _viewResult = _callbackController.Cancel(Convert.ToBase64String(_responseJson), _signature).GetAwaiter().GetResult() as ViewResult;
    }

    [Then("Display cancel page")]
    public void ThenDisplayCancelPage()
    {
        _viewResult.ViewName.Should().Be("~/Plugins/Payments.SimplePay/Views/Callback/Cancel.cshtml");
    }

    [Given("Callback setup valid signature")]
    public void GivenCallbackSetupValidSignature()
    {
        var jsonString = $@"{{""r"":4,""t"":99844942,""e"":""SUCCESS"",""m"":""{DependecyRegistrar.SimplePaySettings.MerchantKey}"",""o"":""101010515680292482600""}}";
        _responseJson = Encoding.UTF8.GetBytes(jsonString);
        _signature = _messageToSendValidator.CalculateSignature(DependecyRegistrar.SimplePaySettings.MerchantKey, jsonString);
    }

    [When("Callback is sent for valid signature")]
    public void WhenCallbackIsSentForValidSignature()
    {
    }

    [Then("No exception is thrown for valid signature")]
    public void ThenNoExceptionIsThrownForValidSignature()
    {
        var action = () => _callbackController.Success(Convert.ToBase64String(_responseJson), _signature).GetAwaiter().GetResult();
        action.Should().NotThrow();
    }

    [Given("Callback setup not valid signature")]
    public void GivenCallbackSetupNotValidSignature()
    {
        var jsonString = $@"{{""r"":4,""t"":99844942,""e"":""SUCCESS"",""m"":""{DependecyRegistrar.SimplePaySettings.MerchantKey}"",""o"":""101010515680292482600""}}";
        _responseJson = Encoding.UTF8.GetBytes(jsonString);
        _signature = _messageToSendValidator.CalculateSignature("FAILED", jsonString);
    }

    [When("Callback is sent for not valid signature")]
    public void WhenCallbackIsSentForNotValidSignature()
    {
    }

    [Then("Throw exception is thrown for not valid signature")]
    public void ThenThrowExceptionIsThrownForNotValidSignature()
    {
        var action = () => _callbackController.Success(Convert.ToBase64String(_responseJson), _signature).GetAwaiter().GetResult();
        action.Should().Throw<SimplePayException>();
    }

}
