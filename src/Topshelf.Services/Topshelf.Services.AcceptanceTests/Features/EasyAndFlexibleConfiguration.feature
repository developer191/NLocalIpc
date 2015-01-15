Feature: Easy and flexible configuration
	In oder to free module creators to add configuration of Agents in each of their config file
	We want to provide one config file per Agent

Scenario: Sample Service uses separate config file
	Given Daemon run with Config read form separated file
		And Daemon is alive
		And I do Sample Request 
	Then I get produced Response with Sample Request
		And Response contains info from pointed Config

	