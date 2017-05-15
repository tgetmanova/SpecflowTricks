@InitializeReaderService
@SetupReader
Feature: RestrictHookingWithTagsFeature
	In order to demonstrate that Before and After hooks 
	can be applied not only for all set, but also
	just for specific Features and Scenarios marked with specific tags,	
	As a Specflow experienced user
	I want to write some scenarios using this possibility


#we expect do some clean up after this scenario
@resetReader	
Scenario: Read newly added book
	Given I have an excellent electronic reader
	And I have uploaded new book to the reader
	Then my reader has at least one book
	And I can read some book


# we expect arrange some ordered pre-condition actions before this scenario
@addSomeBook
Scenario: Read existing book
	Given I have an excellent electronic reader
	And my reader has at least one book
	Then I can read some book


@ensureSomeBookExists
Scenario: Move existing book to trash
	Given I have an excellent electronic reader
	And my reader has at least one book
	Then I can move some book to trash
