using FluentAssertions;
using Nop.Plugin.Payments.SimplePay.Messages.Validators;

namespace Nop.Plugin.Payments.SimplePay.Unit.Test.Messages.Validators
{
    public class MessageToSendValidatorTests
    {
        private MessageToSendValidator _sut;

        public MessageToSendValidatorTests()
        {
            _sut = new MessageToSendValidator();
        }

        [Fact]
        public void CheckCalculatedSignature()
        {
            string message = "{\"salt\":\"c1ca1d0e9fc2323b3dda7cf145e36f5e\",\"merchant\":\"PUBLICTESTHUF\",\"orderRef\":\"101010516348232058105\",\"currency\":\"HUF\",\"customerEmail\":\"sdk_test@otpmobil.com\",\"language\":\"HU\",\"sdkVersion\":\"SimplePayV2.1_Payment_PHP_SDK_2.0.7_190701:dd236896400d7463677a82a47f53e36e\",\"methods\":[\"CARD\"],\"total\":\"25\",\"timeout\":\"2021-10-30T12:30:11+00:00\",\"url\":\"https:\\/\\/sdk.simplepay.hu\\/back.php\"}";
            string merchantKey = "FxDa5w314kLlNseq2sKuVwaqZshZT5d6";
            string expectedResult = "gcDJ8J7TyT1rC/Ygj/8CihXaLwniMWRav09QSEMQUnv5TbYaEDvQAuBE1mW3plvZ";

            string result = _sut.CalculateSignature(merchantKey, message);

            result.Should().Be(expectedResult);
        }
    }
}
