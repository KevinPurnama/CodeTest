
Code-Test-UI is a front-end solution for the TAL Coding Test. It is an angular front end that hooks up to a .NET Core Web API back-end. The front end performs basic data validation, while the back end will execute the data calculations to return a premium quote.

In this implementation, the following assumptions were made:
- The solution is meant to demonstrate Senior Developer level capabilities and therefore should demonstrate Production code traits like decoupling, security, performance and maintainability.
- All input data collected may eventually end up in a database, so proper data cleansing is required. 
- Names cannot contain numbers but can contain special symbols, for example "XÆA-Xii".
- Customers should not be allowed to submit unrealistic data like negative insured sums, negative ages, date of birth beyond 100 years, and future dates.
- Customers should not be allowed to submit mismatched birth dates and ages, with the age calculation to be based on the current system time.
- Only basic UI window dressing is required to support the MVP.