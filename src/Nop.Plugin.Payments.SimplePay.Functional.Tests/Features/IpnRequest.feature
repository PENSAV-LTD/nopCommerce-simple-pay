Feature: Simple Pay Ipn request's tests

Simple Pay Ipn request call.

Scenario: Validate Ipn request
    Given IpnRequest setup for Validate
    When IpnRequest is sent for Validate
    Then Response is Validate

Scenario: Finished Ipn request
    Given IpnRequest setup for Finished
    When IpnRequest is sent for Finished
    Then Response is Finished
    And Order set as payed

Scenario: Authorized Ipn request
    Given IpnRequest setup for Authorized
    When IpnRequest is sent for Authorized
    Then Response is Authorized

Scenario: Reversed Ipn request
    Given IpnRequest setup for Reversed
    When IpnRequest is sent for Reversed
    Then Response is Reversed

Scenario: Cancelled Ipn request
    Given IpnRequest setup for Cancelled
    When IpnRequest is sent for Cancelled
    Then Response is Cancelled

Scenario: Timeout Ipn request
    Given IpnRequest setup for Timeout
    When IpnRequest is sent for Timeout
    Then Response is Timeout

#Example
#{
#"salt":"223G0O18VAqdLhQYbJz73adT36YzLtak",
#"orderRef":"101010515680292482600",
#"method":"CARD",
#"merchant":"PUBLICTESTHUF",
#"finishDate":"2019-09-09T14:46:18+0200",
#"paymentDate":"2019-09-09T14:41:13+0200",
#"transactionId":99844942,
#"status":"FINISHED"
#}
