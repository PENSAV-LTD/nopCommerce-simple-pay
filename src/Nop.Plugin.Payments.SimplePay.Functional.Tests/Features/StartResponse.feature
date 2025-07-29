Feature: Simple Pay start response's tests

Simple pay start response.

# Test Throw exception if it doesn't get right response
Scenario: Throw exception if it doesn't get right response
    Given StartResponse setup BadRequest
    When StartRequest is sent for BadRequest
    Then Response throws exception for BadRequest

# Test Payment url is included in the html page
# Test Error and errorCodes in the response
# Test validate signature from http header
Scenario: Validate signature from http header
    Given StartResponse setup valid signature
    When StartRequest is sent for ValidResponse
    Then Response has valid signarture in the header

# Test Throw expception if signature is not sent in the header
Scenario: Throw exception if signature is not sent in the header
    Given StartResponse setup without signature
    When StartRequest is sent for ValidResponse
    Then Response throws exception
# Example
# {
#     "salt":"KAC6ZRUacmQit98nFKOpjXgkwdC0Grzl",
#     "merchant":"PUBLICTESTHUF",
#     "orderRef":"101010515680292482600",
#     "currency":"HUF",
#     "transactionId":99844942,
#     "timeout":"2019-09-11T21:14:08+02:00",
#     "total":25.0,
#     "paymentUrl":"https://sandbox.simplepay.hu/pay/pay/pspHU/8f4oKRec5R1B696xlxbOcj1jRhhABA2pwSLQDPW60zoGSDWzDU"
# }
