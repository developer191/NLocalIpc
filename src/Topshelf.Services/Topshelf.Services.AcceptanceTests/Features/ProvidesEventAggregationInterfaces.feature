Feature: Provides Event Aggregation Interfaces
	In oder to cross process eventing
	As a Developer User
	We want to this functionality wrapped by standart Event Aggregators

Scenario:  I can publish and subscrive using Prism Event Aggregator
	Given Daemon loaded
		And Subscribe on "Shortcut" Transport event
	When "Shortcut" Transport event published
	Then Subsriber notified

