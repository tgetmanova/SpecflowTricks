@ContextExtensionsDemoFeatureTag
Feature: ContextExtensionsDemoFeature
	In order to demonstrate test data management within Specflow context
	As a Specflow experienced user
	I want to write some scenarios using this possibility


Scenario: Add new book to the reader keeping it in the Specflow context
	Given I have electronic reader
	And I want to add new book with properties title 'Physics for the 9th grade', author 'Saveliev', count of pages '456'
	When I add this book to the reader
	Then new book should be added to the reader with correct properties
