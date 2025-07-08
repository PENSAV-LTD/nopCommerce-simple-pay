using Nop.Plugin.Payments.SimplePay.Functional.Tests.Drivers;
using Nop.Plugin.Payments.SimplePay.Functional.Tests.Support;
using Nop.Plugin.Payments.SimplePay.Models.Requests;
using Nop.Plugin.Payments.SimplePay.Processes;
using Nop.Plugin.Payments.SimplePay.Settings;

namespace Nop.Plugin.Payments.SimplePay.Functional.Tests.StepDefinitions
{
    [Binding]
    public class SimplePayStartRequestsTestsStepDefinitions
    {

        public SimplePayStartRequestsTestsStepDefinitions(
            StartRequestDriver startRequestDriver,
            IHttpClientFactory httpClientFactory
            )
        {
            _startRequestDriver = startRequestDriver;
            _httpClientFactory = httpClientFactory as FakeHttpClientFactory;
        }

        private string _merchantKey;
        private readonly StartRequestDriver _startRequestDriver;
        private readonly FakeHttpClientFactory _httpClientFactory;

        [Given("Merchant key is set as {string}")]
        public void GivenMerchantKeyIsSetAs(string merchantKey)
        {
            _merchantKey = merchantKey;
        }

        [StepDefinition("StartRequest is sent")]
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
            DependecyRegistrar.SimplePaySettings.HasDetailedItems = true;
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

        [Then("Shipping cost is filled")]
        public void ThenShippingCostIsFilled()
        {
            var request = _startRequestDriver.GetStartRequest();
            request.ShippingCost.Should().Be(Convert.ToInt32(OrderProvider.Order.OrderShippingInclTax));
        }

        [Then("Discount value is filled with order discount value")]
        public void ThenDiscountsValueIsFilledWithOrderDiscountValue()
        {
            var request = _startRequestDriver.GetStartRequest();
            request.Discount.Should().Be(Convert.ToInt32(OrderProvider.Order.OrderDiscount));
        }

        [Then("Urls field are always filled with the proper urls")]
        public void ThenUrlsFieldAreAlwaysFilledWithTheProperUrls()
        {
            var request = _startRequestDriver.GetStartRequest();
            request.Urls.Should().NotBeNull();
            request.Urls.Success.Should().Be("http://localhost/simplepaycallback/success");
            request.Urls.Fail.Should().Be("http://localhost/simplepaycallback/fail");
            request.Urls.Cancel.Should().Be("http://localhost/simplepaycallback/cancel");
            request.Urls.Timeout.Should().Be("http://localhost/simplepaycallback/timeout");
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

        [Given("Order is ready to pay with default currency")]
        public void GivenOrderIsReadyToPayWithDefaultCurrency()
        {
            DependecyRegistrar.SimplePaySettings.IsDefaultCurrencyUsed = true;
            DependecyRegistrar.SimplePaySettings.DefaultCurrency = "HUF";
        }

        [Then("Default currency is used in the request")]
        public void ThenDefaultCurrencyIsUsedInTheRequest()
        {
            var request = _startRequestDriver.GetStartRequest();
            request.Currency.Should().Be(DependecyRegistrar.SimplePaySettings.DefaultCurrency);
        }

        [Given("Order is ready to pay with order's currency")]
        public void GivenOrderIsReadyToPayWithOrdersCurrency()
        {
            DependecyRegistrar.SimplePaySettings.IsDefaultCurrencyUsed = false;
        }

        [Then("Order's currency is used in the request")]
        public void ThenOrdersCurrencyIsUsedInTheRequest()
        {
            var request = _startRequestDriver.GetStartRequest();
            request.Currency.Should().Be(OrderProvider.CustomerCurrencyCode);
        }

        [Then("Default payment methods are filled in the request")]
        public void ThenDefaultPaymentMethodsAreFilledInTheRequest()
        {
            var request = _startRequestDriver.GetStartRequest();
            request.Methods.Should().NotBeNull();
            request.Methods.Contains("CARD").Should().BeTrue();
        }

        [Given("Order is ready to pay with two step payment")]
        public void GivenOrderIsReadyToPayWithTwoStepPayment()
        {
            DependecyRegistrar.SimplePaySettings.IsTwoStep = true;
        }

        [Then("TwoStep is true in the request")]
        public void ThenTwoStepIsTrueInTheRequest()
        {
            var request = _startRequestDriver.GetStartRequest();
            request.TwoStep.Should().BeTrue();
        }

        [Given("Order is ready to pay with no two step payment")]
        public void GivenOrderIsReadyToPayWithNoTwoStepPayment()
        {
            DependecyRegistrar.SimplePaySettings.IsTwoStep = false;
        }

        [Then("TwoStep is false in the request")]
        public void ThenTwoStepIsFalseInTheRequest()
        {
            var request = _startRequestDriver.GetStartRequest();
            request.TwoStep.Should().BeFalse();
        }

        [Then("SdkVersion is filled in the request")]
        public void ThenSdkVersionIsFilledInTheRequest()
        {
            var request = _startRequestDriver.GetStartRequest();
            request.SdkVersion.Should().Be(SimplePaySettings.SDK_VERSION);
        }

        [Given("Order is ready to pay with extra percentage")]
        public void GivenOrderIsReadyToPayWithExtraPercentage()
        {
            DependecyRegistrar.SimplePaySettings.AddExtraPercentageToOrderTotal = 15;
        }

        [Then("AddExtraPercentage is added to order total in the request")]
        public void ThenAddExtraPercentageIsAddedToOrderTotalInTheRequest()
        {
            var request = _startRequestDriver.GetStartRequest();
            var orderTotal = OrderProvider.Order.OrderTotal * (1 + DependecyRegistrar.SimplePaySettings.AddExtraPercentageToOrderTotal/100);
            request.Total.Should().Be(Convert.ToInt32(orderTotal));
        }

        [Given("Order is ready to pay with extra amount")]
        public void GivenOrderIsReadyToPayWithExtraAmount()
        {
            DependecyRegistrar.SimplePaySettings.AddExtraToOrderTotal = 1000;
        }

        [Then("AddExtra is added to order total in the request")]
        public void ThenAddExtraIsAddedToOrderTotalInTheRequest()
        {
            var request = _startRequestDriver.GetStartRequest();
            var orderTotal = OrderProvider.Order.OrderTotal + DependecyRegistrar.SimplePaySettings.AddExtraToOrderTotal;
            request.Total.Should().Be(Convert.ToInt32(orderTotal));
        }

        [Given("Order is ready to pay with sandbox mode")]
        public void GivenOrderIsReadyToPayWithSandboxMode()
        {
            DependecyRegistrar.SimplePaySettings.UseSandbox = true;
        }

        [Then("Sandbox url is used in the request")]
        public void ThenSandboxUrlIsUsedInTheRequest()
        {
            var request = _startRequestDriver.GetStartRequest();
            _httpClientFactory.Url.AbsoluteUri.Should().Be(SimplePaySandboxUrls.START_URL);
        }

        [Then("Production url is used in the request")]
        public void ThenProductionUrlIsUsedInTheRequest()
        {
            var request = _startRequestDriver.GetStartRequest();
            _httpClientFactory.Url.AbsoluteUri.Should().Be(SimplePayUrls.START_URL);
        }

        [Then("Items array are filled with all items in the request")]
        public void ThenItemsArrayAreFilledWithAllItemsInTheRequest()
        {
            var request = _startRequestDriver.GetStartRequest();
            request.Items.Should().NotBeNull();
            request.Items.Count.Should().Be(OrderProvider.OrderItems.Count);
        }

        [Given("Order is ready to pay without detailed items")]
        public void GivenOrderIsReadyToPayWithoutDetailedItems()
        {
            DependecyRegistrar.SimplePaySettings.HasDetailedItems = false;
            DependecyRegistrar.SimplePaySettings.OneItemName = "Test Product";
        }

        [Then("Items array is filled with one item in the request")]
        public void ThenItemsArrayIsFilledWithOneItemInTheRequest()
        {
            var request = _startRequestDriver.GetStartRequest();
            request.Items.Should().NotBeNull();
            request.Items.Count.Should().Be(1);
            request.Items[0].Title.Should().Be(DependecyRegistrar.SimplePaySettings.OneItemName);
            request.Items[0].Amount.Should().Be(OrderProvider.OrderItems.Sum(i => i.Quantity));
            request.Items[0].Price.Should().Be(OrderProvider.OrderItems.Sum(i => i.PriceInclTax));
        }

    }
}
