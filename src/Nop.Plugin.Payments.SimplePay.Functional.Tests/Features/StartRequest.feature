Feature: Simple Pay Start request's tests
/*
Test configured merchant key is used in the request
Test Signature is added to header
Test items array is filled with gross prices
Test tax of items are always 0
Test invoice data is filled with order's invoice data
Test delivery information is filled with order's delivery information
Test if HasDetaiedItems is not selected, then shippingCost is always filled
Test if discount's value is always 0
Test urls field are always filled with the proper urls
Test if IsDefaultCurrencyUsed is selected, default currency used instead of order's currency
Test if IsDefaultCurrencyUsed is not selected, order's currency used instead of default currency
Test DefaultPaymentMethods is filled in the request
Test if IsTwoStep is selected, value of the twoStep is true
Test if IsTwoStep is not selected, value of the twoStep is false
Test SdkVersion is filled in the request
Test if UseSandbox selected, simplepay sandbox url is used
Test if AddExtraPercentage is not 0, then add given percentage to order total
Test if AddExtra is not 0, then add given extra to order total
Test if HasDetailedItems is selected, then items array are filled all the items in the request
Test if HasDetailedItems is not selected, then only one item in the items array.

Example
{
    "salt":"126dac8a12693a6475c7c24143024ef8",
    "merchant":"PUBLICTESTHUF",
    "orderRef":"101010515680292482600",
    "currency":"HUF",
    "customerEmail":"sdk_test@otpmobil.com",
    "language":"HU",
    "sdkVersion":"SimplePayV2.1_Payment_PHP_SDK_2.0.7_190701:dd236896400d7463677a82a47f53e36e",
    "methods":[
        "CARD" // "WIRE"
    ],
    "twoStep":false,
    "total":"25",
    "timeout":"2019-09-11T19:14:08+00:00",
    "url":"https:\/\/sdk.simplepay.hu\/back.php",
    "invoice":{
        "name":"SimplePay V2 Tester",
        "company":"",
        "country":"hu",
        "state":"Budapest",
        "city":"Budapest",
        "zip":"1111",
        "address":"Address 1",
        "address2":"Address 2",
        "phone":"06203164978"
    },
    "discount":"5",
    "shippingCost":12,
    "items":[
        {
            "ref":"Product ID 2",
            "title":"Product name 2",
            "desc":"Product description 2",
            "amount":"2",
            "price":"5",
            "tax":"0"
        }
    ],
    "urls":{
        "success":"https://sdk.simplepay.hu/success.php",
        "fail":"https://sdk.simplepay.hu/fail.php",
        "cancel":"https://sdk.simplepay.hu/cancel.php",
        "timeout":"https://sdk.simplepay.hu/timeout.php"
    }
}

*/