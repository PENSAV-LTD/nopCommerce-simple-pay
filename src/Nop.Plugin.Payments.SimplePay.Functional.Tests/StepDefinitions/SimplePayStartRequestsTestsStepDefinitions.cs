using System;
using Nop.Plugin.Payments.SimplePay.Models.Requests;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Plugin.Payments.SimplePay.Transactions;
using Reqnroll;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.StepDefinitions
{
    [Binding]
    public class SimplePayStartRequestsTestsStepDefinitions
    {
        private readonly SimplePaySettings _simplePaySettings;
        private readonly SimplePayStartRequest _simplePayStartRequest;
        private StartRequest _startRequest;

        public SimplePayStartRequestsTestsStepDefinitions(
            SimplePaySettings simplePaySettings,
            SimplePayStartRequest simplePayStartRequest
            )
        {
            _simplePaySettings = simplePaySettings;
            _simplePayStartRequest = simplePayStartRequest;
        }

        [Given("I set the merchant key as {string}")]
        public void GivenISetTheMerchantKeyAs(string merchantKey)
        {
            _simplePaySettings.MerchantKey = merchantKey;
        }

        [When("I have a SimplePayStartRequest object")]
        public void WhenIHaveASimplePayStartRequestObject()
        {
            _startRequest = _simplePayStartRequest.CreateStartRequest();
        }

        [Then("I should see the merchant key as {string} in the request")]
        public void ThenIShouldSeeTheMerchantKeyAsInTheRequest(string merchantKey)
        {
            _startRequest.Merchant.Should().Be(merchantKey);
        }
    }
}
