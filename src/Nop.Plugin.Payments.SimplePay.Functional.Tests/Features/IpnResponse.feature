Feature: Simple Pay Ipn response's tests

Simple Pay Ipn response call.

Scenario: Send response for Ipn request that contains ReceiveDate
    Given Setup IpnRequest
    Then Response's string contains ReceiveDate

Scenario: Validate signature in Response's HTTP header
    Given Setup IpnRequest
    Then Response contains valid signature in HTTP header


#Example
#{
#"salt":"223G0O18VAqdLhQYbJz73adT36YzLtak",
#"orderRef":"101010515680292482600",
#"method":"CARD",
#"merchant":"PUBLICTESTHUF",
#"finishDate":"2019-09-09T14:46:18+0200",
#"paymentDate":"2019-09-09T14:41:13+0200",
#"transactionId":99844942,
#"status":"FINISHED",
#"receiveDate":"2019-09-09T14:46:20+0200"
#}
