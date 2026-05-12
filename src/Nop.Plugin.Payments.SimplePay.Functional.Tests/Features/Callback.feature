Feature: Simple Pay Callback's tests

Simple Pay Callback's tests.

# Test Success callback
Scenario: Test Success callback
    Given Callback setup for success response
    When Callback is sent for success response
    Then Display success page

#Test Fail callback
Scenario: Test Fail callback
    Given Callback setup for fail response
    When Callback is sent for fail response
    Then Display failed page

#Test Timeout callback
Scenario: Test Timeout callback
    Given Callback setup for timeout response
    When Callback is sent for timeout response
    Then Display timeout page

#Test Cancel callback
Scenario: Test Cancel callback
    Given Callback setup for cancel response
    When Callback is sent for cancel response
    Then Display cancel page

#Test validate signature from http header
Scenario: Test validate valid signature from http header
    Given Callback setup valid signature
    When Callback is sent for valid signature
    Then No exception is thrown for valid signature

Scenario: Test validate not valid signature from http header
    Given Callback setup not valid signature
    When Callback is sent for not valid signature
    Then Throw exception is thrown for not valid signature

#Example
#url:
#https://sdk.simplepay.hu/back.php?r=eyJyIjowLCJ0Ijo5OTg0NDk0MiwiZSI6IlNVQ0NFU1MiLCJtIjoiUFVCTE
#lDVEVTVEhVRiIsIm8iOiIxMDEwMTA1MTU2ODAyOTI0ODI2MDAifQ%3D%3D&s=El%2Fnvex9TjgjuORI63gEu5I5miGo4CS
#AD5lmEpKIxp7WuVRq6bBeh1QdyEvVGSsi
#
#{
#"r":0,
#"t":99844942,
#"e":"SUCCESS",
#"m":"PUBLICTESTHUF",
#"o":"101010515680292482600"
#}
