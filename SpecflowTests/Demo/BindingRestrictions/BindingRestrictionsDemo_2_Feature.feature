Feature: BindingRestrictionsDemo another, advanced Feature
	In order to demonstrate that binding methods can be applied only for specific steps with the same wording
	As a Specflow experienced user
	I want to write some scenarios using this possibility


Scenario: Reading the electronic book with the reader with advanced settings
	# This step is specific for the current feature:
	Given I have uploaded book 'Der schwarze Obelisk' to my electronic reader
	And I turned on my electronic reader
