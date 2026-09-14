---
description: "Add a new feature end-to-end through the .NET layers"
---

# Add a Feature End-to-End

Follow these steps in order. Apply the `dotnet-server` backend rules for naming, layering, and error-handling conventions at each step.

1. Add or extend an EF entity: scaffolded entities live in `MyApp.Data/ContextModels`; add an interface in `MyApp.Models` and app-specific behavior via a partial class implementing that interface in `MyApp.Data/AppModels`.
2. Add a repository interface in `MyApp.Models/Repositories`, with the implementation in `MyApp.Data/ContextModels`.
3. Register the repository in `MyApp.Data/DependencyInjection.cs` (`AddData`).
4. Add business logic/services in `MyApp.Business/Services` and register them in `MyApp.Business/DependencyInjection.cs` (`AddBusiness`).
5. Add request/response DTOs in `MyApp.Contracts/Requests` or `MyApp.Contracts/Responses`.
6. Add a thin controller in `MyApp.API` and a handler in `MyApp.API/Handlers` that returns `ServiceResult<T>` via `ServiceResultHandlerBase.ExecuteAsync`; register the handler in `AddApiHandlers()`.
7. Add MSTest coverage in `MyApp.Tests`.
