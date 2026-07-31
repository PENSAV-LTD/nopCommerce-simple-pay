Feature: Simple Pay finish request's test

/*
Test call finish with appropriate data
Test OriginalTotal is equiualent with order's total
Test ApproveTotal is less or equal then OriginalTotal
Test Currency is the original start request's currency
Test Signature is added to header

Example
{
"salt":"a182f12e696d483985133e299c245b83",
"merchant":"PUBLICTESTHUF",
"orderRef":"101010515680496082852",
"originalTotal":"25",
"approveTotal":"15",
"currency":"HUF",
"sdkVersion":"SimplePayV2.1_Payment_PHP_SDK_2.0.7_190701:dd236896400d7463677a82a47f53e36e"
}
*/