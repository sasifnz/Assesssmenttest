Feature: Todo
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Get all available todos
	Given I connected to the api 
	When I make a request to All Todos
	Then I should get total count of 200 Todos

Scenario: Get at 99th todo 
	Given I connected to the api
	When I make a request for 99 Todo
	Then I should get title as 'neque voluptates ratione'

Scenario: Add a new todo
	Given I connected to the api
	When  I add a new todo to the list
	Then  the count should be updated to 201
