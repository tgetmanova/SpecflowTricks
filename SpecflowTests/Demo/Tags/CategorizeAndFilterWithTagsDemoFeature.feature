@theMostImportantTestsSet
@mandatoryForRegressionTesting
Feature: CategorizeAndFilterWithTagsFeature
	In order to demonstrate how we can filter and group test scenarios
	with Specflow tags that are mapped to 'category'/'trait' of unit test	
	As a Specflow experienced user
	I want to write some scenarios using this possibility


# This scenario is in scope of 'The most important tests',
# 'mandatory for regression', marked as 'running quickly'
@quickRun
@positive
Scenario Outline: Read book
	Given I have electronic reader with book '<BookTitle>'
	And I turned on my reader
	When I open book '<BookTitle>'
	Then Book '<BookTitle>' should be opened
# Verification for feature-level tags
	And Feature tags contain 'theMostImportantTestsSet,mandatoryForRegressionTesting' 
# Verification for scenario-level tags
	And Scenario tags contain 'quickRun,positive' 
# Verification for concrete example tags
	And if Scenario example '<BookTitle>' is 'The history of Russia' then Scenario tags also contain 'ForSmokeTesting'

Examples: 
| BookTitle              |
| The history of Hungary |
| The history of Italy   |

# We can select e.g. only one test case (example) for Test Run with "ForSmokeTesting" filter (category/trait)
@ForSmokeTesting
Examples: 
| BookTitle             |
| The history of Russia |