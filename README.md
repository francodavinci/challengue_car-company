# Car Company API

## 📋 Description
REST API for automobile sales management with data analysis by distribution center. Implements Clean Architecture with Domain-Driven Design (DDD) and modern design patterns.

## 🏗️ Architecture
- **Clean Architecture** with clear layer separation
- **Domain-Driven Design (DDD)** with entities, exceptions and interfaces
- **Repository Pattern** for data access
- **Use Cases Pattern** for business logic
- **Dependency Injection** for dependency inversion

## 🚀 Technologies
- **.NET 8** - Main framework
- **ASP.NET Core Web API** - REST API
- **xUnit** - Testing framework
- **Moq** - Mocking for tests
- **Swagger/OpenAPI** - API documentation
- **Microsoft.Extensions.Logging** - Structured logging

## 📊 Quality Metrics
- **83.51%** line coverage
- **83.33%** branch coverage
- **61 tests** implemented
- **100%** tests passing
- **Logging** of execution time for each endpoint

## 🛠️ Installation and Execution

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or VS Code
- Git

### Installation Steps
1. **Clone the repository**
   ```bash
   git clone https://github.com/francodavinci/challengue_car-company
   cd challengue_car-company
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Run tests**
   ```bash
   dotnet test
   ```

4. **Run application**
   ```bash
   dotnet run --project CarCompany.API
   ```

### Environment Variables
```bash
# For development
ASPNETCORE_ENVIRONMENT=Development

# For production
ASPNETCORE_ENVIRONMENT=Production
```

## 📚 API Endpoints

### 1. Create Sale
```http
POST /api/sales
Content-Type: application/json

{
  "distributionCenterID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "carType": 0
}
```

**Response:**
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "car": {
    "model": 0,
    "price": 8000
  },
  "distributionCenterID": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "date": "2024-01-15T10:30:00Z"
}
```

### 2. Get Total Sales
```http
GET /api/sales
```

**Response:**
```json
{
  "totalSales": 25000,
  "totalUnits": 3
}
```

### 3. Sales by Distribution Center
```http
GET /api/sales/{distributionCenterId}
```

**Response:**
```json
{
  "totalAmount": 15000,
  "totalUnits": 2
}
```

### 4. Sales Percentages by Center
```http
GET /api/sales/percentage-by-center
```

**Response:**
```json
{
  "centerPercentages": [
    {
      "centerName": "North Center",
      "modelPercentages": {
        "0": {
          "units": 2,
          "percentage": 66.67
        },
        "1": {
          "units": 1,
          "percentage": 33.33
        }
      }
    }
  ]
}
```

### 5. Health Check
```http
GET /api/health
```

**Response:**
```json
{
  "status": "Healthy",
  "timestamp": "2024-01-15T10:30:00Z",
  "version": "1.0.0",
  "environment": "Development"
}
```

### 6. Detailed Health Check
```http
GET /api/health/detailed
```

**Response:**
```json
{
  "status": "Healthy",
  "timestamp": "2024-01-15T10:30:00Z",
  "version": "1.0.0",
  "environment": "Development",
  "systemInfo": {
    "machineName": "DESKTOP-ABC123",
    "processorCount": 8,
    "workingSet": 52428800,
    "privateMemory": 41943040,
    "virtualMemory": 1073741824,
    "uptime": "00:15:30"
  }
}
```

### 7. Built-in Health Checks
```http
GET /health
GET /health/ready
GET /health/live
```

## 🧪 Testing

### Run Tests
```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific tests
dotnet test --filter "Category=Unit"
```

### Generate Coverage Report
```bash
# Install reporting tool
dotnet tool install -g dotnet-reportgenerator-globaltool

# Generate HTML report
reportgenerator -reports:"TestResults\*\coverage.cobertura.xml" -targetdir:"CoverageReport" -reporttypes:"Html"
```

### Test Structure
- **Domain Tests** - Entities and exceptions
- **Repository Tests** - Data access
- **Use Case Tests** - Business logic
- **Integration Tests** - End-to-end
- **Controller Tests** - API endpoints

## 📁 Project Structure

```
CarCompany/
├── CarCompany.API/                 # Presentation Layer
│   ├── Controllers/
│   │   └── SalesController.cs
│   └── Program.cs
├── CarCompany.Application/         # Application Layer
│   ├── DTOs/                      # Data Transfer Objects
│   └── UseCases/                  # Use Cases
├── CarCompany.Domain/             # Domain Layer
│   ├── Entities/                  # Business entities
│   ├── Enums/                     # Enumerations
│   ├── Exceptions/                # Domain exceptions
│   └── Interfaces/                # Repository contracts
├── CarCompany.Infrastructure/     # Infrastructure Layer
│   └── Repositories/              # Repository implementations
└── CarCompany.Tests/              # Tests
    ├── Controllers/               # Controller tests
    ├── Domain/                    # Domain tests
    ├── Repositories/              # Repository tests
    └── UseCases/                  # Use case tests
```

## 🎯 Main Features

### ✅ Implemented Functionality
- **Sales Management** - Create and query sales
- **Data Analysis** - Totals and percentages by center
- **Input Validation** - ModelState validation
- **Error Handling** - Domain exceptions
- **Logging** - Execution time for each method
- **Testing** - Comprehensive coverage

### ✅ Design Patterns
- **Repository Pattern** - Data access abstraction
- **Use Case Pattern** - Business logic encapsulation
- **Dependency Injection** - Dependency inversion
- **Clean Architecture** - Separation of responsibilities

### ✅ Code Quality
- **Clean Code** - SOLID principles applied
- **Naming Conventions** - Consistent conventions
- **Error Handling** - Robust error handling
- **Logging** - Structured and useful logging

## 📈 Performance Metrics

| Metric | Value |
|---------|-------|
| **Line Coverage** | 83.51% |
| **Branch Coverage** | 83.33% |
| **Total Tests** | 61 |
| **Passing Tests** | 100% |
| **Execution Time** | Automatic logging |

## 🔧 Development

### Useful Commands
```bash
# Build solution
dotnet build

# Clean solution
dotnet clean

# Restore packages
dotnet restore

# Run application in development mode
dotnet run --project CarCompany.API --environment Development

# Run tests with detail
dotnet test --logger "console;verbosity=normal"
```

### Swagger/OpenAPI
Interactive documentation is available at:
- **Development:** `https://localhost:5001` or `http://localhost:5000`
- **Swagger UI:** Graphical interface to test endpoints
- **OpenAPI Spec:** Complete API specification

## 📝 Technical Decisions

### Why Clean Architecture?
- **Clear separation of responsibilities**
- **Improved testability**
- **Long-term maintainability**
- **Independence** from external frameworks

### Why DDD?
- **Domain modeling** based on business rules
- **Domain exceptions** for error handling
- **Entities** that encapsulate behavior

### Why Use Cases?
- **Encapsulation** of business logic
- **Code reusability**
- **Easier and more specific testing**

## 🚀 Next Steps

### Future Improvements
- [ ] **Caching** for frequent queries
- [ ] **Pagination** in listing endpoints
- [ ] **Authentication** and authorization
- [ ] **Real database** (SQL Server/PostgreSQL)
- [ ] **Docker** for containerization
- [ ] **CI/CD** pipeline

### Optimizations
- [ ] **Async/Await** In case of have real database , api external services, I/O operations.
- [ ] **Response compression**
- [ ] **Rate Limiting** for protection
- [ ] **Health Checks** for monitoring

## 📞 Contact

For questions or technical support, contact the development team.

Developer: francocicirelli97@gmail.com

---
