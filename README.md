# GymApi
Scalable backend built with .Net 8, implementing the Generic Repository Pattern and Service Layer. Featuring strict business logic validation with FluentValidation and DTO mapping with AutoMapper.

This is a personal project developed to build a scalable backend for tracking gym routines. My main goal was to apply modern .Net 8 design patterns and ensure that that the business logic is fully decoupled from the data access layer.

Architecture: 
  - Generic Repository Pattern: I centralized data access to avoid code duplication and to make unit testing easier in the future.

  - Service Layer: All business logic resides here. This ensures that the API controllers remain thin and only handle HTTP requests.

  - FluentValidation: I chose decoupled validation to keep DTOs clean. This allowed me to handle complex rules—like preventing duplicate usernames—before hitting the database.

  - Keyed Services: I implemented this native .NET 8 feature to manage multiple service implementations more efficiently within the Dependency Injection container.

Stack:
  - Runtime: .NET 8.
  
  - Database: SQL Server with EF Core (Code First).

  - Mapping: AutoMapper for clean DTO management.

  - Documentation: Swagger UI for endpoint testing.

