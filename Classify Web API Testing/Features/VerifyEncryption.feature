@Suite:Classify-Web-API
Feature: VerifyEncryption

As a end user
I want to verify that my messages are being encrypted and Decrypting correctly
Using Cøassify API Service call

Background: 
	Given Backend Classify Web Service is set up 

@subSuite:keyGen @integration-tests @key-gen
Scenario: Verify that keygen functionality is working
	When I generate a key of length 26 using KeyGen functionality
	Then Key of length 26 is generated successfully

@subSuite:keyGen @integration-tests @encrypt
Scenario: Verify that Encrypt functionality is working
	When I encrypt "this is my message" and key is "ThisIsMyKey"
	Then The message is encrypted
	And Encrypted message is generated

#TODO
##Will add some negative path scenarios if time allows
##Also right now, APIs are not working throwinf 503
