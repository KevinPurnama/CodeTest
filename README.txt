DESCRIPTION

Code-Test-UI is a front-end solution for the TAL Coding Test. It is an angular front end that hooks up to a .NET Core Web API back-end. The front end performs basic data validation, while the back end will execute the data calculations to return a premium quote and return a dynamic list of possible occupations to select.

ASSUMPTIONS

In this implementation, the following assumptions were made:
- The solution is meant to demonstrate Senior Developer level capabilities and therefore should demonstrate Production code traits like decoupling, security, performance and maintainability.
- All input data collected may eventually end up in a database, so proper data cleansing or use of a database interfacing framework is required. The design also takes into consideration the data that might need to be changed in future.
- Names cannot contain numbers but can contain special symbols, for example "XÆA-Xii".
- Customers should not be allowed to submit unrealistic data like negative insured sums, negative ages, date of birth beyond 100 years, and future dates.
- Customers should not be allowed to submit mismatched birth dates and ages, with the age calculation to be based on the current system time.
- Customer age can be limited to a maximum of 110 years.
- Routing and pages won't be required as there is only one page.
- Only basic UI window dressing is required to support the MVP. A purchase path style (step-by-step input prompt) was considered due to the fact that auto-submit was supposed to be triggered on selection of Occupation, however for simplicity and ease of testing I opted for the more conventional form layout with Occupation placed as the last field to help direct the course of user interaction.
- Generating a database is outside the scope of this exercise. A database will be simulated in memory to drive the premium calculations on the server side.
- Only a subset of test cases is required to demonstrate capability. Due to time constaints not all positive, negative and edge case scenarios will be implemented as unit tests.
- The list of Occupation names is dynamic data and therefore it should not be represented as an enumeration in the system.

INSTRUCTIONS

1. Run the Code-Test-API Solution in Visual Studio 2022. Confirm the API url and port number.
2. If required, update the .\Code-Test-UI\src\environments\environment.ts with an updated apiEndpoint value.
3. Navigate to the Code-Test-UI directory
4. Run ng serve and navigate to the given url.