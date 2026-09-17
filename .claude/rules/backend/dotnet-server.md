---
paths:
  - "**/*.cs"
  - "**/*.csproj"
  - "**/*.sln"
  - "**/*.sql"
  - "**/appsettings*.json"
---
# .NET Server Architecture and C# Rules

## Architecture

- Use a hybrid N-tier and Clean Architecture style with explicit project boundaries.
- Expected projects:
  - `MyApp.API`: presentation layer, controllers, routing, filters, and API composition.
  - `MyApp.Infrastructure`: root dependency injection registration for Business and Data.
  - `MyApp.Business`: services, business logic, external API coordination, and business DI extension methods.
  - `MyApp.Data`: repositories, EF Core context access, internal API data access, and data DI extension methods.
  - `MyApp.Models`: interfaces and internal models shared across layers.
  - `MyApp.Contracts`: request and response DTOs at application boundaries when present.
  - `MyApp.Common`: constants, enums, helper methods, and custom exceptions shared by multiple projects.
  - `MyApp.Tests`: unit tests for the application.
- Only Infrastructure references Business and Data directly.
- API must not reference Business or Data directly.
- API communicates through handlers, interfaces, dependency injection, and boundary DTOs.
- Keep dependency injection centralized in Infrastructure via an `AddInfrastructure` extension method, which calls Business's `AddBusiness` and Data's `AddData` extension methods along with any external library DI extensions.
- Business and Data must each contain a file named `DependencyInjection.cs` holding their DI extension method.

## Folder and Project Governance

- Do not rename, add, or remove top-level projects (`MyApp.API`, `MyApp.Business`, `MyApp.Common`, `MyApp.Contracts`, `MyApp.Data`, `MyApp.Infrastructure`, `MyApp.Models`, `MyApp.Tests`) without approval.
- Place new code inside the existing standard folder for its layer. New DTOs go in `MyApp.Contracts/Requests` or `MyApp.Contracts/Responses`, never a new top-level folder.
- If a new top-level project genuinely seems necessary, stop and ask the user for approval before creating it.

## Layer Responsibilities

- Controllers should stay thin. They should route requests, call handlers or service result handlers, and return action results.
- Service result handlers coordinate service calls, logging, exception handling, and ServiceResult creation.
- Business services contain business rules and use repositories or external APIs to complete use cases.
- Repositories contain data-access logic only. Do not put business rules in repositories.
- Models contain interfaces and internal domain-facing contracts.
- Contracts contain DTOs for request and response boundaries only.
- DTOs must not contain business logic.

## Service Result Handlers

- Controllers call handlers only; they never inject or call Business services or repositories directly.
- Every endpoint has a handler under a `Handlers` folder (for example `MyApp.API/Handlers`).
- Handlers return `ServiceResult<T>`, using `ServiceResultHandlerBase.ExecuteAsync` for start/end logging and exception-to-HTTP mapping.
- Business services throw `BusinessRuleException` or `EntityNotFoundException`; handlers map them to `StatusCode`, `ErrorCode`, and `ErrorMessage`.
- Use `MyApp.Common.Constants.ErrorCodes` for error codes; do not use magic strings.
- Do not catch exceptions in controllers.
- Register handlers in `AddApiHandlers()`; register service interfaces in `AddBusiness()`.
- Service interfaces live in `MyApp.Models/Services`; implementations live in `MyApp.Business/Services`.
- Controllers map handler results with `ToActionResult()` so the UI receives `isSuccess`, `statusCode`, `errorCode`, `errorMessage`, and `value`.

```csharp
// Controller pattern
IActionResult result = (await _handler.HandleAsync(request, cancellationToken)).ToActionResult();
return result;
```

## Entity Framework and Data Access

- Use EF Core scaffolding with `--no-onconfiguring`.
- Keep scaffolded context and entity files separated from partial app model extensions.
- Extend generated entities via partial classes in an app-specific models folder (for example `MyApp.Data/AppModels`), and give each extended model a matching interface in `MyApp.Models`.

```csharp
// Bad
public partial class MyClass
// Good
public partial class MyClass : IMyClass
```

- Classes that derive from `DbContext`, and classes used in a `DbSet<>`, live in a `ContextModels` folder in the Data project (for example `MyApp.Data/ContextModels`).
- Repository interfaces belong in Models.
- Repository implementations belong in Data.
- Register repository interfaces and implementations with scoped lifetime.
- Use separate application databases for new applications.
- Every table must have CreateBy, CreateDate, UpdateBy, and UpdateDate fields.
- Use foreign keys within the same application database where appropriate for EF Core relationships.
- Evaluate external database foreign keys case by case. Do not assume cross-database foreign keys are appropriate.
- CreatedBy and UpdatedBy should be nvarchar to support employee IDs or process names.
- When creating a local development database (LocalDB or otherwise), specify an explicit file path for the data and log files instead of accepting the default, which drops them loose in the user's home directory. Keep them in a consistent, dedicated location alongside the project's other database assets, such as a `LocalDB` subfolder next to existing schema/seed scripts.

## C# Style

- Use explicit types. Do not use `var`.
- Use PascalCase for public types, methods, and constants.
- Use camelCase for local variables and method parameters.
- Prefix interfaces with `I`.
- Prefix private member variables with `_` and use camelCase after the underscore.
- Use descriptive class and method names.
- Class names should be nouns and represent one responsibility.
- Method names should usually contain a verb and clearly describe the action.
- Avoid method names such as `Go`, `Complete`, `Get`, `Process`, `DoIt`, `On_Init`, and `Page_Load` unless the name is genuinely precise in context.
- Avoid method names containing `And`, `If`, or `Or`; these often indicate multiple responsibilities.

```csharp
// BAD
var repo = new ApplicationRepository();

// GOOD
IApplicationRepository repository = new ApplicationRepository();
```

- Prefer assigning the return value to an explicitly typed local variable, then returning that variable. Do not use inline `return <expression>;` when the expression spans multiple operations (for example `await` plus mapping).

```csharp
// Preferred
IActionResult result = (await _handler.HandleAsync(id, cancellationToken)).ToActionResult();
return result;
// Avoid
return (await _handler.HandleAsync(id, cancellationToken)).ToActionResult();
```

## Error Handling

- Use custom exceptions for expected business-rule failures.
- Do not throw generic `Exception` for known business cases.
- Let system exceptions bubble to the general exception handler unless the current layer can handle them safely.
- Fail fast and loudly for unrecoverable errors.
- Recoverable errors should have bounded retry or fallback behavior.
- Ignorable errors should be rare and logged.

## Authentication and Hosting

- For internal same-origin IIS deployments, prefer Windows Authentication when the browser and API share protocol, hostname, and port.
- Do not pass spoofable user identifiers such as employee ID through JSON payloads when the server can derive identity from authentication context.
- For cross-origin React and API deployments, configure CORS intentionally and narrowly.
- Production CORS should only allow required production origins.
- Apply `[Authorize]` where end-user identity is required.
- Use IHttpContextAccessor or the repository's standard web helper package to access authenticated user context.

## Contracts Layer

- Put external boundary schemas in Contracts when the application exposes request and response shapes.
- Use separate `Requests` and `Responses` folders.
- Include `Request` or `Response` in DTO file names.
- Business maps repository models and external data into DTOs.
- Contracts should obscure repository schemas, adapt data for UI needs, expose derived data, and use business-friendly names.
