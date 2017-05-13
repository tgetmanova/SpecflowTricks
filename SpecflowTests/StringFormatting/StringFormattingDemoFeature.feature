﻿Feature: StringFormattingDemoFeature
	In order to demonstrate string formatting
	As a Specflow experienced user
	I want to write some scenarios using this possibility


# Hard-coded formatting: 'The history of Hungary': single quotes can be used within string formatting
Scenario: Attempt to open non existing book
	Given I have electronic reader with only one book 'Cookbook'
	And I turned on the reader
	When I try to open book 'The history of Hungary'
	Then I get validation error that contains 'The book 'The history of Hungary' has not been found in electronic reader storage'


# Hard-coded formatting: "Cookbook": double quotes can be used within string formatting
Scenario: Attempt to open another book when already reading
	Given I have electronic reader with only one book 'Cookbook'
	And I turned on the reader
	And I opened the 'Cookbook' book
	And I added 'The history of Italy' book to the reader
	When I try to open book 'The history of Italy'
	Then I get validation error that contains 'Electronic reader is reading "Cookbook" book now'


# Examples data can be injected as placeholders into string formatting
Scenario Outline: Attempt to open non existing books again
	Given I have electronic reader with only one book 'Cookbook'
	And I turned on the reader
	When I try to open book '<BookTitle>'
	Then I get validation error that contains 'The book '<BookTitle>' has not been found in electronic reader storage'

Examples: 
| BookTitle              |
| The history of Hungary |
| The history of Italy   |


# If we generate data only on the code-level, the information can be passed from the inside and be validated. 
# There can be several placeholders {0}, {1} ... since string.Format() c# method allows to pass params object[] args
# Of course, it is not visible at the scenario level
Scenario: Attempt to open non existing book with random title
	Given I have electronic reader with only one book 'Cookbook'
	And I turned on the reader
	When I try to open book with random title
	Then I get validation error formatted as 'The book '{0}' has not been found in electronic reader storage'