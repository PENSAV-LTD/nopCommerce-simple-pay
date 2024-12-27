using System;
using System.Net.WebSockets;
using System.Text.Json;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Support;
using Nop.Plugin.Payments.SimplePay.Models.Requests;
using Nop.Plugin.Payments.SimplePay.Processes;
using Nop.Plugin.Payments.SimplePay.Settings;
using Nop.Plugin.Payments.SimplePay.Transactions;
using Reqnroll;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.StepDefinitions
{
    [Binding]
    public class SimplePayStartRequestsTestsStepDefinitions
    {

        public SimplePayStartRequestsTestsStepDefinitions(
            StartRequestDriver startRequestDriver
            )
        {
            _startRequestDriver = startRequestDriver;
        }

        private string _merchantKey;
        private readonly StartRequestDriver _startRequestDriver;

        [Given("Merchant key is set as {string}")]
        public void GivenMerchantKeyIsSetAs(string merchantKey)
        {
            _merchantKey = merchantKey;
        }

        [When("StartRequest is sent")]
        public void WhenStartRequestIsSent()
        {
            if (string.IsNullOrEmpty(_merchantKey))
            {
                _startRequestDriver.SendStartRequest();
            }
            else
            {
                _startRequestDriver.SendStartRequest(_merchantKey);
            }
        }

        [Then("Merchant key is {string} in the request")]
        public void ThenMerchantKeyIsInTheRequest(string merchantKey)
        {
            var request = _startRequestDriver.GetStartRequest();
            request.Merchant.Should().Be(merchantKey);
        }

        [Given("Request is about to be sent")]
        public void GivenRequestIsAboutToBeSent()
        {
        }

        [Then("Signature is added to header")]
        public void ThenSignatureIsAddedToHeader()
        {
            var headers = _startRequestDriver.GetHeaders();
            headers.Should().ContainKey("Signature");
        }

        [Given("Order is ready to pay")]
        public void GivenOrderIsReadyToPay()
        {
            throw new PendingStepException();
        }

        [Then("Items array is filled with gross prices")]
        public void ThenItemsArrayIsFilledWithGrossPrices()
        {
            throw new PendingStepException();
        }

    }
}
