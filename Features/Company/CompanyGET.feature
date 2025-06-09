Feature: CompanyGET

Gets a company data

@RestAPI
Scenario: 01 - Checking the Company GET endpoint with a valid data
	Given I obtained the access token using 'Company:Business User'
	When I send a GET request to Get company Id endpoint to a valid company code '57595'
	Then I verify 200 HTTP response code
	And I verify the company data is correct
	| CompanyName     | CompanyCode | TerminationDate |
	| Automation Test | 57595       |                 |

@RestAPI
Scenario: 02 - Checking the Company GET API endpoint with an invalid company code
	Given I obtained the access token using 'Company:Business User'
	When I send a GET request to Get company Id endpoint to an invalid company code '57557'
	Then I verify 403 HTTP response code
	And I verify 'Forbidden' response message
	And I verify the error message 'The user does not have access to the company'
	
@RestAPI
Scenario: 03 - Checking the Company GET API endpoint without access token
	Given I obtained the access token using 'Company:Business User'
	When I send a GET request to Get company Id endpoint without access token to a valid company code '57595'
	Then I verify 401 HTTP response code
	And I verify 'Unauthorized' response message
	#

	