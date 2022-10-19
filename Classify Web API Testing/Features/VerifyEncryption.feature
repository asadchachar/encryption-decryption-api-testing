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
	When I encrypt this is my message and key is ThisIsMyKey
	Then The message is encrypted
	And Encrypted message is generated

@subSuite:keyGen @integration-tests @decrypt
Scenario: Verify that Decrypt functionality is working
	And I encrypt this is my message and key is ThisIsMyKey
	And The message is encrypted
	And Encrypted message is generated
	When I decrypt the Encrypted message using key ThisIsMyKey
	Then I get this is my message in response


@subSuite:keyGen @e2e-tests @encrypt @decrypt @key-gen
Scenario Outline: Verify that Encrypt Decrypt functionality is working End to End
	When I generate a key of length <keyLength> using KeyGen functionality
	And Key of length <keyLength> is generated successfully
	And I encrypt <message> with the generated key
	And The message is encrypted
	And Encrypted message is generated
	When I decrypt the Encrypted message using the generated key
	Then I get <message> in response
	Examples:
	| keyLength | message                                                    |
	| 30        | Please encrypt me                                          |
	| 100       | This is confidential message                               |
	| 2000      | This is very long and secret communication. Please Encrypt |

#TODO:
#Will add some negative path scenarios if time allows
