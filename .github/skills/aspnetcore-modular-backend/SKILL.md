---
name: aspnetcore-modular-backend
description: Implement backend features in this ASP.NET Core modular monolith using its endpoint, CQRS, repository, API response, and migration conventions.
---

# ASP.NET Core Modular Backend

Use this skill when adding or changing backend features in this repository. Follow existing module boundaries and naming conventions; do not introduce a new architecture for a single feature.

## Module layers

Keep responsibilities in their existing layers:

- `*.Application.Contract`: request/response DTOs, commands, queries, and application-facing contracts.
- `*.Domain`: entities, domain behavior, and domain-owned repository abstractions.
- `*.Application`: command/query handlers and FluentValidation validators.
- `*.Persistence`: EF Core configuration, DbContexts, repository implementations, and module migration registration.
- `*.Presentation`: HTTP endpoints only.

Do not make the Domain project depend on Application or Application.Contract.

## Files and naming

Keep each endpoint, command, query, and handler in its own file. Keep command and query repositories in separate files as well.

Use the module's existing feature folders and names, for example:

```text
<Module>.Application.Contract/
  DTOs/<Feature>/
  UseCase/<Feature>/Commands/Create<Feature>Command.cs
  UseCase/<Feature>/Queries/Get<Feature>Query.cs
  Interface/<Feature>/I<Feature>QueryRepository.cs

<Module>.Application/
  UseCase/<Feature>/Commands/Create<Feature>CommandHandler.cs
  UseCase/<Feature>/Queries/Get<Feature>QueryHandler.cs

<Module>.Domain/
  Entities/<Feature>.cs
  Interface/I<Feature>CommandRepository.cs

<Module>.Persistence/
  Configuration/<Feature>Configuration.cs
  Repositories/<Feature>/<Feature>CommandRepository.cs
  Repositories/<Feature>/<Feature>QueryRepository.cs

<Module>.Presentation/
  Endpoints/<Feature>/Create<Feature>Endpoint.cs
  Endpoints/<Feature>/Get<Feature>Endpoint.cs
```

Adjust names and folders to match the neighboring feature in the same module.

## CQRS

- Define each command as a separate `ICommand<TResponse>` record in Application.Contract.
- Define each query as a separate `IQuery<TResponse>` record in Application.Contract.
- Implement each command with its own `ICommandHandler<TCommand, TResponse>` class.
- Implement each query with its own `IQueryHandler<TQuery, TResponse>` class.
- Return `Ardalis.Result` from handlers and propagate failures; do not convert failures into success-shaped defaults.
- Put FluentValidation validators in separate, clearly named files. Validate request data at the application boundary.

## Endpoints and API responses

- Each endpoint must be in its own file and implement `IEndpoint`.
- Follow the existing pattern: nested `EndPoint : BaseEndpoint, IEndpoint`, map with `MapGet`, `MapPost`, `MapPut`, or `MapDelete`, and apply the module's API tag.
- Send commands and queries through `ICommandBus` and `IQueryBus`; do not call repositories from Presentation.
- Use `BaseEndpoint.Ok(...)`, `BadRequest(...)`, `NotFound(...)`, and `Unauthorized(...)` so responses use the shared `ApiResponse` contract where supported. Do not return `Results.Ok` or `Results.BadRequest` for normal feature responses.
- Use `MessageHelper.Format` with `AppMessages` and `AppEntity` for standard create/edit/delete messages. Return data with `Ok(data)` or `Ok(data, message)` when the caller needs the created or queried DTO.
- Require authorization for user-specific endpoints. Derive the user ID from the authenticated claims; never trust a user ID supplied in the request body or route.
- Check handler result status before returning success. Map missing resources to `NotFound` when the endpoint's established behavior supports it.

## Frontend API documentation

- When adding, changing, or removing an API endpoint, update `README-Frontend.md` to match. Document the HTTP method and route, authentication requirements, and any request or response details needed by frontend consumers.

## Repository conventions

- Keep repository interfaces in the layer that owns the contract. Domain-specific command repository contracts belong in Domain.Interface when required by the domain; they should inherit the domain generic `ICommandRepository<TEntity>` without creating a Domain-to-Application dependency.
- Keep query repository contracts under the module's `Application.Contract.Interface` following the existing module pattern.
- Keep command and query repository implementations in separate Persistence files.
- Reuse the module's generic `CommandRepository<TEntity>` or `QueryRepository<TEntity>` where the context type and behavior are compatible. If they are not compatible, add a module-specific generic base in Persistence rather than changing unrelated modules.
- Keep EF Core access and persistence details out of Presentation and handlers.

## EF Core migrations

- Register migrations with the module's write DbContext in that module's Persistence dependency-injection setup using `IModuleMigration` and `DbContextMigration<TWriteDbContext>`.
- Let the shared host migration runner apply registered module migrations; do not hard-code a module migration call in `Program.cs`.
- Create a migration only when the EF model differs from its latest snapshot. Never add an empty migration to silence a warning.
- Review generated migration operations before keeping them. Treat `DropColumn`, `DropTable`, and narrowing/required `AlterColumn` operations as potentially destructive; do not apply them without confirming the data and intended behavior.
- Do not suppress `PendingModelChangesWarning`. Generate the missing migration and verify that it represents the intended model change.

## Verification

After implementation:

1. Build the smallest affected project; build the Web project when shared wiring or module registration changes.
2. Verify new endpoints, commands, handlers, and repository implementations are discovered and registered through the existing module scanning and DI patterns.
3. If a migration is created, inspect both `Up` and `Down` and verify the model snapshot was updated.
