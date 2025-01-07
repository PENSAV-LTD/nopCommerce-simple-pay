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
                _startRequestDriver.SendStartRequest(OrderProvider.Order);
            }
            else
            {
                _startRequestDriver.SendStartRequest(OrderProvider.Order, _merchantKey);
            }
        }

        [When("StartRequest is sent with a customer without billing address")]
        public void WhenStartRequestIsSentWithACustomerWithoutBillingAddress()
        {
            _startRequestDriver.SendStartRequest(OrderProvider.OrderWithoutBillingAddress);
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
        }

        [Then("Items array is filled with gross prices")]
        public void ThenItemsArrayIsFilledWithGrossPrices()
        {
            var request = _startRequestDriver.GetStartRequest();
            var orderItems = OrderProvider.OrderItems;
            for(var i=0; i < request.Items.Count; i++)
            {
                request.Items[i].Price.Should().Be(orderItems[i].PriceInclTax);
            }
        }

        [Then("Tax of items are always {int}")]
        public void ThenTaxOfItemsAreAlways(int taxRate)
        {
            var request = _startRequestDriver.GetStartRequest();
            for (var i = 0; i < request.Items.Count; i++)
            {
                request.Items[i].Tax.Should().Be(taxRate);
            }
        }

        [Then("Invoice data is filled with customer's data")]
        public void ThenInvoiceDataIsFilledWithCustomersData()
        {
            var request = _startRequestDriver.GetStartRequest();
            VerifyInvoiceDataEqualToCustomerData(request);
        }

        [Then("Invoice data is filled with customer's billing data")]
        public void ThenInvoiceDataIsFilledWithCustomersBillingData()
        {
            var request = _startRequestDriver.GetStartRequest();
            VerifyInvoiceDataEqualToCustomerBillingData(request);
        }

        [Then("Salt is filled")]
        public void ThenSaltIsFilled()
        {
            var request = _startRequestDriver.GetStartRequest();
            request.Salt.Should().NotBeNullOrEmpty();
            request.Salt.Length.Should().Be(32);
        }

        private static void VerifyInvoiceDataEqualToCustomerBillingData(StartRequest request)
        {
            request.Invoice.Name.Should().BeEquivalentTo(CustomerAndAddressProvider.BillingAddressFullName);
            request.Invoice.Company.Should().Be(CustomerAndAddressProvider.BillingAddress.Company);
            request.Invoice.Phone.Should().Be(CustomerAndAddressProvider.BillingAddress.PhoneNumber);
            request.Invoice.Country.Should().Be(CustomerAndAddressProvider.Country.TwoLetterIsoCode);
            request.Invoice.State.Should().Be(CustomerAndAddressProvider.StateProvince.Name);
            request.Invoice.City.Should().Be(CustomerAndAddressProvider.BillingAddress.City);
            request.Invoice.Address.Should().Be(CustomerAndAddressProvider.BillingAddress.Address1);
            request.Invoice.Address2.Should().Be(CustomerAndAddressProvider.BillingAddress.Address2);
            request.Invoice.Zip.Should().Be(CustomerAndAddressProvider.BillingAddress.ZipPostalCode);
        }

        private static void VerifyInvoiceDataEqualToCustomerData(StartRequest request)
        {
            request.Invoice.Name.Should().BeEquivalentTo(CustomerAndAddressProvider.CustomerFullName);
            request.Invoice.Company.Should().Be(CustomerAndAddressProvider.Customer.Company);
            request.Invoice.Phone.Should().Be(CustomerAndAddressProvider.Customer.Phone);
            request.Invoice.Country.Should().Be(CustomerAndAddressProvider.Country.TwoLetterIsoCode);
            request.Invoice.State.Should().Be(CustomerAndAddressProvider.StateProvince.Name);
            request.Invoice.City.Should().Be(CustomerAndAddressProvider.Customer.City);
            request.Invoice.Address.Should().Be(CustomerAndAddressProvider.Customer.StreetAddress);
            request.Invoice.Address2.Should().Be(CustomerAndAddressProvider.Customer.StreetAddress2);
            request.Invoice.Zip.Should().Be(CustomerAndAddressProvider.Customer.ZipPostalCode);
        }
    }
}
