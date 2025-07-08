using System;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Support;
using Reqnroll;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.StepDefinitions
{
    [Binding]
    public class SimplePayStartResponsesTestsStepDefinitions
    {
        private readonly HttpClientFactorySettings _httpClientFactorySettings;
        private readonly StartRequestDriver _startRequestDriver;
        private Exception _exception;

        public SimplePayStartResponsesTestsStepDefinitions(
            HttpClientFactorySettings httpClientFactorySettings,
            StartRequestDriver startRequestDriver
            )
        {
            _httpClientFactorySettings = httpClientFactorySettings;
            _startRequestDriver = startRequestDriver;
        }

        [Given("StartResponse setup BadRequest")]
        public void GivenStartResponseSetupBadRequest()
        {
            _httpClientFactorySettings.Url = "https://api.simplepay.com/start";
            _httpClientFactorySettings.ResponseBody = "{\"error\":\"Bad Request\"}";
            _httpClientFactorySettings.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _httpClientFactorySettings.Headers = null; // Assuming no headers for this test
        }

        [When("StartRequest is sent for BadRequest")]
        public void WhenStartRequestIsSentForBadRequest()
        {
            try
            {
                _startRequestDriver.SendStartRequest(OrderProvider.Order);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then("Response throws exception")]
        public void ThenResponseThrowsException()
        {
            _exception.Should().NotBeNull();
            _exception.Message.Should().Contain("Bad Request");
        }
    }
}
