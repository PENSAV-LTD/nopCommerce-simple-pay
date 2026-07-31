Feature: Simple Pay refund request's tests

/*
Test call refund request
Test currency is the same as the original one
Test Signature is added to header

Example
{
"salt":"6a85ef475fa491618a94af9bb0b2065d",
"orderRef":"101010515680496082852",
"merchant":"PUBLICTESTHUF",
"currency":"HUF",
"refundTotal":5,
"sdkVersion":"SimplePayV2.1_Payment_PHP_SDK_2.0.7_190701:dd236896400d7463677a82a47f53e36e"
}
*/