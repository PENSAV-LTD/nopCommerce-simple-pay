using System;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Support;
using Nop.Plugin.Payments.SimplePay.Messages.Validators;
using Nop.Plugin.Payments.SimplePay.Processes;
using Reqnroll;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.StepDefinitions
{
    [Binding]
    public class SimplePayStartResponsesTestsStepDefinitions
    {
        private readonly HttpClientFactorySettings _httpClientFactorySettings;
        private readonly FakeHttpClientFactory _fakeHttpClentFactory;
        private readonly StartRequestDriver _startRequestDriver;
        private readonly IMessageToSendValidator _messageToSendValidator;
        private readonly string _merchantKey = "TEST";
        private Exception _exception;

        public SimplePayStartResponsesTestsStepDefinitions(
            HttpClientFactorySettings httpClientFactorySettings,
            IHttpClientFactory fakeHttpClentFactory,
            StartRequestDriver startRequestDriver,
            IMessageToSendValidator messageToSendValidator
            )
        {
            _httpClientFactorySettings = httpClientFactorySettings;
            _fakeHttpClentFactory = fakeHttpClentFactory as FakeHttpClientFactory;
            _startRequestDriver = startRequestDriver;
            _messageToSendValidator = messageToSendValidator;
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
            _httpClientFactorySettings.StatusCode = System.Net.HttpStatusCode.OK;
        }

        [Given("StartResponse setup valid signature")]
        public void GivenStartResponseSetupValidSignature()
        {
            _httpClientFactorySettings.ResponseBody = "{\"salt\":\"KAC6ZRUacmQit98nFKOpjXgkwdC0Grzl\",\"merchant\":\"PUBLICTESTHUF\",\"orderRef\":\"101010515680292482600\",\"currency\":\"HUF\",\"transactionId\":99844942,\"timeout\":\"2019-09-11T21:14:08+02:00\",\"total\":25.0,\"paymentUrl\":\"https://sandbox.simplepay.hu/pay/pay/pspHU/8f4oKRec5R1B696xlxbOcj1jRhhABA2pwSLQDPW60zoGSDWzDU\"}";
        }

        [When("StartRequest is sent for ValidResponse")]
        public void WhenStartRequestIsSentForValidResponse()
        {
            DependecyRegistrar.SimplePaySettings.MerchantKey = _merchantKey;
            _startRequestDriver.SendStartRequest(OrderProvider.Order, _merchantKey);
        }

        [Then("Response has valid signarture in the header")]
        public void ThenResponseHasValidSignartureInTheHeader()
        {
            var headers = _startRequestDriver.GetResponse().Headers;
            var expectedSignature = _messageToSendValidator.CalculateSignature(_merchantKey, _fakeHttpClentFactory.Settings.ResponseBody);
            headers.Should().NotBeNull();
            headers.Contains("Signature").Should().BeTrue();
            headers.GetValues("Signature").Should().Equal(expectedSignature);
        }

    }
}
