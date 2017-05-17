Feature: DataContextManagementDemoFeature
	In order to demonstrate test data management with context injection
	As a Specflow experienced user
	I want to write some scenarios using this possibility


@contextInjectionDemo
Scenario: Take the reader and add new book
	Given I have my electronic reader
	When I add book with title 'Dependency injection in .NET'
	Then book 'Dependency injection in .NET' is added to the reader


@registerInstanceAsOptionForBookList
Scenario: Take the reader, add book and open this book - option 1
	Given I have my electronic reader
	# This step is from "Common functionality" bindings class:
	And I have added book with title 'Dependency injection in .NET' in the reader
	And I turned my reader on
	# And this step is from "Open functionality" binding class,
	# so we expect that "reader" dependency with newly added (in another binding class)
	# book wll be resolved as shared instance from the container and 
	# provided for us in this step:
	When I open the book 'Dependency injection in .NET'
	Then book 'Dependency injection in .NET' is opened in the reader


# the same as scenario above, but we use another registration in objectContainer in the hook method
@registerTypeAsOptionForBookList
Scenario: Take the reader, add book and open this book - option 2
	Given I have my electronic reader
	And I have added book with title 'Dependency injection in .NET' in the reader
	And I turned my reader on
	When I open the book 'Dependency injection in .NET'
	Then book 'Dependency injection in .NET' is opened in the reader