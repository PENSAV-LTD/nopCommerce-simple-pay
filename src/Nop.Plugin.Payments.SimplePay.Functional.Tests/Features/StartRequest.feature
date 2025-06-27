Feature: Simple Pay Start request's tests

Simple Pay start request call

# Configured merchant key is used in the request
Scenario: Start request call with given merchant key
    Given Merchant key is set as "PUBLICTESTHUF" 
    When  StartRequest is sent
    Then  Merchant key is "PUBLICTESTHUF" in the request

# Signature is added to header
Scenario: Signarture is added to start request call
    Given Request is about to be sent
    When StartRequest is sent
    Then Signature is added to header

# Items array is filled with gross prices
Scenario: Items array is filled with gross prices
    Given Order is ready to pay
    When  StartRequest is sent
    Then  Items array is filled with gross prices
    
# Tax of items are always 0
Scenario: Tax of items are always 0
    Given Order is ready to pay
    When StartRequest is sent
    Then Tax of items are always 0

# Invoice data is filled with customer's data - billing address isn't filled
Scenario: Invoice data is filled with customer's data - billing address isn't filled
    Given Order is ready to pay
    When StartRequest is sent with a customer without billing address
    Then Invoice data is filled with customer's data

# Invoice data is filled with customer's data - billing address is filled
Scenario: Invoice data is filled with customer's data - billing address is filled
    Given Order is ready to pay
    When StartRequest is sent
    Then Invoice data is filled with customer's billing data

# Salt is filled
Scenario: Salt is filled
    Given Order is ready to pay
    When StartRequest is sent
    Then Salt is filled

# Test delivery information is filled with order's delivery information
# ShippingCost is always filled
Scenario: Shipping cost is always filled
    Given Order is ready to pay
    When StartRequest is sent
    Then Shipping cost is filled

# Discount's value is filled with order's discount value
Scenario: Discount value is filled with order discount value
    Given Order is ready to pay
    When StartRequest is sent
    Then Discount value is filled with order discount value

# Test urls field are always filled with the proper urls
Scenario: Urls field are always filled with the proper urls
    Given Order is ready to pay
    When StartRequest is sent
    Then Urls field are always filled with the proper urls

# Test if IsDefaultCurrencyUsed is selected, default currency used instead of order's currency
Scenario: Default currency is used if IsDefaultCurrencyUsed is selected
    Given Order is ready to pay with default currency
    When StartRequest is sent
    Then Default currency is used in the request

# Test if IsDefaultCurrencyUsed is not selected, order's currency used instead of default currency  
Scenario: Order's currency is used if IsDefaultCurrencyUsed is not selected
    Given Order is ready to pay with order's currency
    When StartRequest is sent
    Then Order's currency is used in the request

# Test DefaultPaymentMethods is filled in the request
Scenario: Default payment methods are filled in the request
    Given Order is ready to pay
    When StartRequest is sent
    Then Default payment methods are filled in the request

# Test if IsTwoStep is selected, value of the twoStep is true
Scenario: TwoStep is true if IsTwoStep is selected
    Given Order is ready to pay with two step payment
    When StartRequest is sent
    Then TwoStep is true in the request

# Test if IsTwoStep is not selected, value of the twoStep is false
Scenario: TwoStep is false if IsTwoStep is not selected
    Given Order is ready to pay with no two step payment
    When StartRequest is sent
    Then TwoStep is false in the request

# Test SdkVersion is filled in the request
# Test if UseSandbox selected, simplepay sandbox url is used
# Test if AddExtraPercentage is not 0, then add given percentage to order total
# Test if AddExtra is not 0, then add given extra to order total
# Test if HasDetailedItems is selected, then items array are filled all the items in the request
# Test if HasDetailedItems is not selected, then only one item in the items array.
# 
# Example
# {
#     "salt":"126dac8a12693a6475c7c24143024ef8",
#     "merchant":"PUBLICTESTHUF",
#     "orderRef":"101010515680292482600",
#     "currency":"HUF",
#     "customerEmail":"sdk_test@otpmobil.com",
#     "language":"HU",
#     "sdkVersion":"SimplePayV2.1_Payment_PHP_SDK_2.0.7_190701:dd236896400d7463677a82a47f53e36e",
#     "methods":[
#         "CARD" // "WIRE"
#     ],
#     "twoStep":false,
#     "total":"25",
#     "timeout":"2019-09-11T19:14:08+00:00",
#     "url":"https:\/\/sdk.simplepay.hu\/back.php",
#     "invoice":{
#         "name":"SimplePay V2 Tester",
#         "company":"",
#         "country":"hu",
#         "state":"Budapest",
#         "city":"Budapest",
#         "zip":"1111",
#         "address":"Address 1",
#         "address2":"Address 2",
#         "phone":"06203164978"
#     },
#     "discount":"5",
#     "shippingCost":12,
#     "items":[
#         {
#             "ref":"Product ID 2",
#             "title":"Product name 2",
#             "desc":"Product description 2",
#             "amount":"2",
#             "price":"5",
#             "tax":"0"
#         }
#     ],
#     "urls":{
#         "success":"https://sdk.simplepay.hu/success.php",
#         "fail":"https://sdk.simplepay.hu/fail.php",
#         "cancel":"https://sdk.simplepay.hu/cancel.php",
#         "timeout":"https://sdk.simplepay.hu/timeout.php"
#     }
# }