Feature: Cross-process communication beetwin different modules
	In oder to provide possiblily for module to say somthing to other modules loaded by different Office application
	We want to provide special services

Scenario: Publish and subsribe from one process
	Given Daemon loaded
		And Subscribe on "Shortcut" event
#try create second transport in other appdomain
	When "Shortcut" event published
	Then Subscriber notified

@ignore
Scenario: Publish for one event and subsribe on another from one process
	Given Daemon loaded
		And Subscribe on "Shortcut" event
	When "ContextChanged" event published
		And I wait for responce for a while
	Then Subscriber not notified

@ignore
Scenario: Publish for one event and subsribe from 2 processs
	Given Daemon loaded
		And Subscribe on "Shortcut" event
	When other process runned which publishes "Shortcut" #run process
	Then Subscriber notified

@ignore
Scenario: Transport loads daemon if not loaded
	Given Trasnport created
	Then Daemon is alive
