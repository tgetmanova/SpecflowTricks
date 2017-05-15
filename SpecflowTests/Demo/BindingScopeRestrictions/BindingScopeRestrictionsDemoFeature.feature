Feature: BindingRestrictionsDemoFeature
	In order to demonstrate that binding methods can be applied only for specific steps with the same wording
	As a Specflow experienced user
	I want to write some scenarios using this possibility


Scenario: Reading the electronic book with the reader
	# This step should be implemented as default for all scenarios in feature and the current scenario:
	Given I have uploaded book 'Der schwarze Obelisk' to my electronic reader
	And I turned on my electronic reader
	# This step should be implemented as default for all scenarios and the current scenario:
	When I opened the 'Der schwarze Obelisk' book


Scenario: Reading the paper book - scenario restriction
	Given I have bought book 'Der schwarze Obelisk' in hard-cover
	# This step is should be implemented separately with specific logic for the current scenario:
	When I opened the 'Der schwarze Obelisk' book


# we use tag to mark scenario
@paperFunctionality
Scenario: Reading the paper book - restriction with tag
	Given I have bought book 'Der schwarze Obelisk' in hard-cover
	# This step is should be implemented separately with specific logic for the current scenario:
	When I opened the 'Der schwarze Obelisk' book