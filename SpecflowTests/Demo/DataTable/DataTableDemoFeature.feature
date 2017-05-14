Feature: DataTableDemoFeature
	In order to demonstrate data tables data transformation
	As a Specflow experienced user
	I want to write some scenarios using this possibility


# Use data table to create single instance of Book - only three properies will be initialized
# The rest of properties we will set manually in code already to the "copy" of this instance
Scenario: Add new book to the reader - horizontal table
	Given I have electronic reader
	When I add new book with properties
	| Title                        | Author    | NumberOfPages |
	| Dependency Injection in .NET | M.Seemann | 900           |
	Then new book should be added to the reader
	| Title                        | Author    | NumberOfPages |
	| Dependency Injection in .NET | M.Seemann | 900           |


# The same as the sample above, but table is vertically oriented.
# There no requirements for headers' names
Scenario: Add new book to the reader - vertical table
	Given I have electronic reader
	When I add new book with properties
	| BookProperty  | BookPropertyValue            |
	| Title         | Dependency Injection in .NET |
	| Author        | M.Seemann                    |
	| NumberOfPages | 900                          |
	Then new book 'Dependency Injection in .NET' should be added to the reader


# Use data table to create set of instances of Book
# The same - only three properies will be initialized for all instances
Scenario: Add several books to the reader
	Given I have electronic reader
	When I add several books with properties
	| Title                                 | Author      | NumberOfPages |
	| Dependency Injection in .NET          | M. Seemann  | 900           |
	| CLR via C#                            | J. Richter  | 1300          |
	| Pro C# 5.0 and the .NET 4.5 Framework | A. Troelsen | 1450          |
	Then new books should be added to the reader
	| Title                                 | Author      | NumberOfPages |
	| Dependency Injection in .NET          | M. Seemann  | 900           |
	| CLR via C#                            | J. Richter  | 1300          |
	| Pro C# 5.0 and the .NET 4.5 Framework | A. Troelsen | 1450          |


# We can map our table rows headers' titles to the property names while parsing table in code
Scenario: Add new book to the reader with inner properties specified
	Given I have electronic reader
	When I add new book with all of properties
	| Title                        | Author    | NumberOfPages | StateInReader | ElectronicInfo.DataFormat | ElectronicInfo.SizeInMB |
	| Dependency Injection in .NET | M.Seemann | 900           | Unloaded      | Pdf                       | 45                      |
	Then new book 'Dependency Injection in .NET' should be added to the reader