# Order Module - Directory Structure & File Locations

## Complete File Tree

```
src/Modules/Order/
│
├── Modules.Order.Domain/
│   ├── Modules.Order.Domain.csproj
│   ├── Entities/
│   │   ├── ShoppingCart.cs
│   │   ├── OrderEntity.cs
│   │   └── OrderItem.cs
│   ├── Enums/
│   │   └── OrderStatus.cs
│   ├── Interfaces/
│   │   ├── IOrderRepository.cs
│   │   └── IShoppingCartRepository.cs
│   └── bin/
│   └── obj/
│
├── Modules.Order.Application.Contract/
│   ├── Modules.Order.Application.Contract.csproj
│   ├── DTOs/
│   │   ├── OrderItemDto.cs
│   │   ├── OrderDto.cs
│   │   └── ShoppingCartDto.cs
│   ├── UseCase/
│   │   ├── Orders/
│   │   │   ├── Commands/
│   │   │   │   └── OrderCommands.cs
│   │   │   └── Queries/
│   │   │       └── OrderQueries.cs
│   │   └── ShoppingCarts/
│   │       ├── Commands/
│   │       │   └── ShoppingCartCommands.cs
│   │       └── Queries/
│   │           └── ShoppingCartQueries.cs
│   └── bin/
│   └── obj/
│
├── Modules.Order.Application/
│   ├── Modules.Order.Application.csproj
│   ├── DependencyInjection.cs
│   ├── UseCase/
│   │   ├── Orders/
│   │   │   ├── Commands/
│   │   │   │   └── OrderCommandHandlers.cs
│   │   │   └── Queries/
│   │   │       └── OrderQueryHandlers.cs
│   │   └── ShoppingCarts/
│   │       ├── Commands/
│   │       │   └── ShoppingCartCommandHandlers.cs
│   │       └── Queries/
│   │           └── ShoppingCartQueryHandlers.cs
│   ├── Mapping/
│   │   └── OrderMappingConfig.cs
│   └── bin/
│   └── obj/
│
├── Modules.Order.Persistence/
│   ├── Modules.Order.Persistence.csproj
│   ├── DependencyInjection.cs
│   ├── Context/
│   │   ├── OrderWriteDbContext.cs
│   │   └── OrderReadDbContext.cs
│   ├── Configuration/
│   │   ├── ShoppingCartConfiguration.cs
│   │   ├── OrderConfiguration.cs
│   │   └── OrderItemConfiguration.cs
│   ├── Repositories/
│   │   ├── OrderRepository.cs
│   │   └── ShoppingCartRepository.cs
│   ├── Migrations/
│   │   └── (Will be generated)
│   └── bin/
│   └── obj/
│
├── Modules.Order.Presentation/
│   ├── Modules.Order.Presentation.csproj
│   ├── OrderModule.cs
│   ├── Endpoints/
│   │   ├── Orders/
│   │   │   ├── Write/
│   │   │   │   └── OrderWriteEndpoints.cs
│   │   │   └── Read/
│   │   │       └── OrderReadEndpoints.cs
│   │   └── ShoppingCarts/
│   │       ├── Write/
│   │       │   └── ShoppingCartWriteEndpoints.cs
│   │       └── Read/
│   │           └── ShoppingCartReadEndpoints.cs
│   └── bin/
│   └── obj/
│
├── MODULE_SUMMARY.md
├── FILES_CREATED.md
└── INTEGRATION_GUIDE.md
```

---

## Quick File Reference

### Most Important Files

**To Understand the Domain:**
- `Modules.Order.Domain/Entities/ShoppingCart.cs` - Shopping cart domain model
- `Modules.Order.Domain/Entities/OrderEntity.cs` - Order domain model
- `Modules.Order.Domain/Entities/OrderItem.cs` - Order item domain model

**To Understand the API:**
- `Modules.Order.Presentation/Endpoints/Orders/Write/OrderWriteEndpoints.cs` - Order write operations
- `Modules.Order.Presentation/Endpoints/Orders/Read/OrderReadEndpoints.cs` - Order read operations
- `Modules.Order.Presentation/Endpoints/ShoppingCarts/Write/ShoppingCartWriteEndpoints.cs` - Cart write operations
- `Modules.Order.Presentation/Endpoints/ShoppingCarts/Read/ShoppingCartReadEndpoints.cs` - Cart read operations

**To Register Services:**
- `Modules.Order.Presentation/OrderModule.cs` - Main module entry point
- `Modules.Order.Application/DependencyInjection.cs` - Application layer DI
- `Modules.Order.Persistence/DependencyInjection.cs` - Persistence layer DI

**To Understand Commands/Queries:**
- `Modules.Order.Application/UseCase/Orders/Commands/OrderCommandHandlers.cs` - Order command logic
- `Modules.Order.Application/UseCase/Orders/Queries/OrderQueryHandlers.cs` - Order query logic
- `Modules.Order.Application/UseCase/ShoppingCarts/Commands/ShoppingCartCommandHandlers.cs` - Cart command logic
- `Modules.Order.Application/UseCase/ShoppingCarts/Queries/ShoppingCartQueryHandlers.cs` - Cart query logic

**To Configure Database:**
- `Modules.Order.Persistence/Context/OrderWriteDbContext.cs` - Write DbContext
- `Modules.Order.Persistence/Configuration/OrderConfiguration.cs` - Order table mapping
- `Modules.Order.Persistence/Configuration/ShoppingCartConfiguration.cs` - Cart table mapping
- `Modules.Order.Persistence/Configuration/OrderItemConfiguration.cs` - OrderItem table mapping

---

## File Purpose Summary

| File | Purpose | Lines (Approx) |
|------|---------|---|
| ShoppingCart.cs | Domain entity for shopping cart | 50 |
| OrderEntity.cs | Domain entity for order with business logic | 100 |
| OrderItem.cs | Domain entity for order line items | 80 |
| OrderStatus.cs | Enum for order statuses | 10 |
| IOrderRepository.cs | Repository interfaces for orders | 20 |
| IShoppingCartRepository.cs | Repository interfaces for carts | 25 |
| OrderWriteDbContext.cs | EF Core DbContext for writes | 20 |
| OrderReadDbContext.cs | EF Core DbContext for reads | 20 |
| ShoppingCartConfiguration.cs | EF mapping for ShoppingCart | 30 |
| OrderConfiguration.cs | EF mapping for Order | 60 |
| OrderItemConfiguration.cs | EF mapping for OrderItem | 45 |
| OrderRepository.cs | Repository implementations | 50 |
| ShoppingCartRepository.cs | Repository implementations | 80 |
| OrderItemDto.cs | DTOs for order items | 30 |
| OrderDto.cs | DTOs for orders | 40 |
| ShoppingCartDto.cs | DTOs for shopping carts | 25 |
| OrderCommands.cs | CQRS command contracts | 25 |
| OrderQueries.cs | CQRS query contracts | 15 |
| ShoppingCartCommands.cs | CQRS command contracts | 10 |
| ShoppingCartQueries.cs | CQRS query contracts | 12 |
| OrderCommandHandlers.cs | Command handlers with validators | 300+ |
| OrderQueryHandlers.cs | Query handlers with validators | 120 |
| ShoppingCartCommandHandlers.cs | Command handlers with validators | 80 |
| ShoppingCartQueryHandlers.cs | Query handlers | 90 |
| OrderMappingConfig.cs | Mapster configuration | 20 |
| OrderModule.cs | Module registration | 25 |
| DependencyInjection.cs (App) | DI setup for application | 50 |
| DependencyInjection.cs (Persistence) | DI setup for persistence | 60 |
| OrderWriteEndpoints.cs | Write endpoints for orders | 200+ |
| OrderReadEndpoints.cs | Read endpoints for orders | 130 |
| ShoppingCartWriteEndpoints.cs | Write endpoints for carts | 70 |
| ShoppingCartReadEndpoints.cs | Read endpoints for carts | 120 |

---

## File Count by Layer

```
Domain Layer:           6 files
Persistence Layer:      7 files
Application.Contract:   7 files
Application Layer:      6 files
Presentation Layer:     8 files
Project Files:          5 .csproj files
Documentation:          3 .md files
─────────────────────────────────
TOTAL:                 42 files
```

---

## Adding to Existing Solution

### Update Solution Structure

Your src/Modules directory should now look like:

```
src/Modules/
├── FileStore/
├── Identity/
├── Order/          ← NEW MODULE
│   ├── Modules.Order.Domain/
│   ├── Modules.Order.Application.Contract/
│   ├── Modules.Order.Application/
│   ├── Modules.Order.Persistence/
│   └── Modules.Order.Presentation/
└── Product/
```

### Update Your Solution File

Add projects to AShop.slnx:
```xml
<Project Path="src/Modules/Order/Modules.Order.Domain/Modules.Order.Domain.csproj" />
<Project Path="src/Modules/Order/Modules.Order.Application.Contract/Modules.Order.Application.Contract.csproj" />
<Project Path="src/Modules/Order/Modules.Order.Application/Modules.Order.Application.csproj" />
<Project Path="src/Modules/Order/Modules.Order.Persistence/Modules.Order.Persistence.csproj" />
<Project Path="src/Modules/Order/Modules.Order.Presentation/Modules.Order.Presentation.csproj" />
```

---

## Build and Compile

### From Command Line

```bash
# Restore all packages
dotnet restore

# Build the solution
dotnet build

# Build with detailed output
dotnet build --verbose

# Clean and rebuild
dotnet clean
dotnet build
```

### From Visual Studio

1. Open AShop.slnx in Visual Studio
2. Rebuild Solution (Right-click → Rebuild Solution)
3. Check Output for any build errors

---

## Testing the Module

### 1. Unit Test Structure (Create these separately)

```
Tests/
├── Modules.Order.Application.Tests/
│   ├── Commands/
│   ├── Queries/
│   └── Handlers/
├── Modules.Order.Domain.Tests/
│   ├── Entities/
│   └── Factories/
└── Modules.Order.Persistence.Tests/
    ├── Repositories/
    └── Contexts/
```

### 2. Integration Test Structure

```
Tests/
└── Modules.Order.Integration.Tests/
    ├── Endpoints/
    ├── Database/
    └── Fixtures/
```

---

## File Sizes (Estimated)

| Component | Approx Size |
|-----------|------------|
| Domain Layer | ~20 KB |
| Persistence Layer | ~25 KB |
| Application.Contract | ~15 KB |
| Application Layer | ~80 KB |
| Presentation Layer | ~45 KB |
| **Total** | **~185 KB** |

---

## CI/CD Integration

### Build Configuration

Add to your CI/CD pipeline:

```yaml
- name: Build Order Module
  run: dotnet build src/Modules/Order/Modules.Order.Presentation/Modules.Order.Presentation.csproj

- name: Run Tests
  run: dotnet test

- name: Publish
  run: dotnet publish -c Release
```

---

## Deployment Checklist

- [ ] All projects compile without warnings
- [ ] Unit tests pass
- [ ] Integration tests pass
- [ ] Database migrations created and tested
- [ ] Connection string configured in appsettings.json
- [ ] Module registered in Program.cs
- [ ] Endpoints verified in Swagger
- [ ] Error handling tested
- [ ] Performance validated
- [ ] Documentation updated

---

## Module is Ready! ✅

All 42 files have been created and are ready for integration into your ASP.NET Core solution.
