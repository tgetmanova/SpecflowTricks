# Specflow Tricks

This is short guide that describes how to use some Specflow amazing features
- __Data tables__ transformation
  - Direct table parsing
  - Using `Specflow Assist` library to create certain instances and set of instances
  - Using `Specflow Assist` library and customize instance newly created with `StepsArgumentTransformation` attribute
- __Extensions__ for Feature and Scenario contexts to manage test data
- Creating your own __test data context__ and share it between multiple steps binding classes
- __Strings formatting__ is steps:
  - inline formatting with placeholders passed from Scenario-level data
  - inline formatting with placeholders passed from code-level data
- Tags
  - Filtering usage: Using tags to restrict ___Hook___ method binding (`Before`, `After`). Using multiple tags in ___Hook___ methods
  - Categorizing usage: Using tags to mark
    - Feature
    - Scenario
    - Scenario Example
- Just some nice things about Specflow
  - Usage of multiple binding attributes on method in order to bing it to several steps
  - Usage of `Scope` attribute in order to restrict method binding to some concrete Feature/Scenario step
