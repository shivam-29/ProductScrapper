# Product Scrapper Azure Function

Product Scrapper is an Azure Function designed to scrape product attributes from HTML content provided by a given URL. The function is built using Clean Architecture principles, incorporates HotChocolate for handling GraphQL queries, and employs centralized NuGet package management for easy maintenance.

## Features

- **Clean Architecture**: Follows the Clean Architecture principles for better maintainability and separation of concerns.

- **HotChocolate for GraphQL**: Allows clients to submit GraphQL queries to request specific product attributes from the HTML content.

- **Azure Function**: Built as an Azure Function, making it scalable and serverless.

- **Centralized NuGet Package Management**: NuGet packages are centrally managed for consistency and easy updates.

## Getting Started

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/yourusername/ProductScrapper.git
   cd ProductScrapper

# Project Structure

The project follows the Clean Architecture pattern, dividing the application into layers:

- **EntryApp Layer**: GraphQL Azure Function.
- **Application Layer**: Use cases and business logic.
- **Infrastructure Layer**: HTML scraping logic and external dependencies.
- **Domain Layer**: It contains the Entities

```plaintext
/ProductScrapper
|-- src
|   |-- ProductScrapper.Application
|   |   |-- ...
|   |
|   |-- ProductScrapper.Domain
|   |   |-- ...
|   |
|   |-- ProductScrapper.EntryApp
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


