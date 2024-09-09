Feature: Start call
This feature is tested the start call of Simple Pay API

Scenario: Request body is properly set
    Given User collect the items in their cart and start the transaction
    When  Call start method of API
    Then  Proper response received

