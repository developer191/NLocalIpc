Feature: Seamless and Robust usage experience
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Use Agent Client whithout manual of run Daemon
	Given I requested 'SampleValidAgent'
	When I use service of 'SampleValidAgent'
	Then All is Ok

Scenario: Use Agents Client whithout manual of run Daemon from 2 threads
	Given I requested 'SampleValidAgent' in first Thread
		And I requested 'SampleValidAgent' in second Thread
	When I use service of 'SampleValidAgent' in first Thread
		And I use service of 'SampleValidAgent' in second Thread
	Then All is Ok

Scenario: Use Agent Client which Fails does not broke Valid Agent
	Given I requested 'SampleValidAgent'
		And I requested 'SampleInvalidAgent'
	When I use service of 'SampleInvalidAgent'
		When I use service of 'SampleValidAgent'
	Then Valid Agents works well 


