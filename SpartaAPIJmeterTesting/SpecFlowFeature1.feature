Feature: Fixer API Tests
	

@mytag
Scenario: Perform Health Check
	Given I have a valid API Key
	When I perform a health check
	Then I should see a status of 'OK'
