## Architecture

CRM Lite follows a layered clean architecture:
- `CRM_Lite.Domain` contains entities (`Customer`, `Product`, `Purchase`, `PurchaseItem`) and the `ProductCategory` enum.
- `CRM_Lite.Application` holds DTOs, service/repository interfaces, FluentValidation rules, and Mapster-powered services orchestrating business logic.
- `CRM_Lite.Infrastructure` wires Entity Framework Core (PostgreSQL) with `AppDbContext`, repositories, and migrations.
- `CRM_Lite.Web` exposes REST endpoints via controllers, registers the DI graph, FluentValidation auto-validation, Swagger, and a global exception handler.
- Test projects (`CRM_Lite.UnitTest`) cover service logic with xUnit/Moq and include an xUnit.

## Main Technologies and Tools

- .NET 8 — modern development platform 
- Entity Framework Core — ORM for database access with PostgreSQL
- PostgreSQL 16 (local via Docker)
- FluentValidation, Mapster, SequentialGuid
- Swagger/Swashbuckle
- xUnit, Moq, Shouldly (unit tests)
- SequentialGuid — GUID generation for entities
- Mapster — DTO ↔ Entity mapping

## Features

- Customer management: create customers, filter by birthday, list recent buyers.
- Product management: create categorized products with pricing metadata.
- Purchase tracking: record purchases with line items and aggregate purchased categories per customer.
- Robust validation and centralized exception handling with ProblemDetails responses.
- Pagination helpers (`PagedResult`, `PagedResponse`) for consistent API outputs.

## Getting Started

1. Install .NET 8 SDK and Docker Desktop.
2. Clone the repository and navigate to `CRM_Lite`.
3. Start PostgreSQL via Docker (see Docker Setup below).
4. Update connection strings if needed (`CRM_Lite.Web/appsettings.*.json`).
5. Apply migrations (`dotnet ef database update ...`).
6. Run the API:
   ```
   dotnet run --project CRM_Lite.Web/CRM_Lite.Web.csproj
   ```
7. Swagger UI will be available at `https://localhost:7077/swagger` (port per launch profile).

## Running Tests

Execute the unit test suite:
```
dotnet test CRM_Lite.UnitTest/CRM_Lite.UnitTest.csproj
```

## Database Migrations

- **Add migration**
  ```
  dotnet ef migrations add MigrationName -p CRM_Lite.Infrastructure -s CRM_Lite.Web
  ```
- **Update database**
  ```
  dotnet ef database update -p CRM_Lite.Infrastructure -s CRM_Lite.Web
  ```

> Ensure the connection string in `CRM_Lite.Web/appsettings.Development.json` points to the running PostgreSQL instance before applying migrations.

## Docker Setup

1. Ensure Docker Desktop is running.
2. From the repo root start the database:
   ```
   docker compose up -d
   ```
3. Stop and clean up:
   ```
   docker compose down
   ```
   Add `-v` to remove the `postgres_data` volume if you need a fresh database.

The compose file provisions PostgreSQL 16 with credentials matching the development connection string, exposing port `5432`. Apply migrations after the container is healthy.

