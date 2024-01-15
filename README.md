# ProductScrapper


ProductScrapper Azure Function with HotChocolate
ProductScrapper is an Azure Function designed to scrape product attributes from HTML content provided by a given URL. The function is built using Clean Architecture principles and incorporates HotChocolate for handling GraphQL queries. Centralized NuGet package management is implemented for seamless package updates and maintenance.

Features
Clean Architecture: The project structure follows the principles of Clean Architecture, promoting separation of concerns and maintainability.

HotChocolate for GraphQL: GraphQL queries can be submitted to the Azure Function, allowing clients to request specific product attributes from the HTML content.

Azure Function: The application is built as an Azure Function, making it scalable and serverless.

Centralized NuGet Package Management: NuGet packages are centrally managed to ensure consistency and ease of updates.

# Project Folder Structure
/ProductScrapper
|-- src
|   |-- ProductScrapper.FunctionApp
|   |   |-- ...
|   |
|   |-- ProductScrapper.Application
|   |   |-- ...
|   |
|   |-- ProductScrapper.Infrastructure
|       |-- ...
|
|-- tests
|   |-- ProductScrapper.UnitTests
|   |   |-- ...
|   |
|   |-- ProductScrapper.IntegrationTests
|       |-- ...
|
|-- ProductScrapper.sln
|-- README.md

