# TinyLedger API

TinyLedger is a simple ledger system built using **.NET 9**, following Clean Architecture principles and aligned with RESTful best practices, including Richardson Maturity Model Level 2.

---

## ✅ Architecture Overview

This project follows **Clean Architecture**, using four main layers:

- **Domain**: Core business models and interfaces (e.g. `Transaction`, `ILedgerRepository`)
- **Application**: Use cases and business rules, orchestrated using [MediatR](https://github.com/jbogard/MediatR)
- **Infrastructure**: In-memory implementation of persistence interfaces
- **API**: Entry point (ASP.NET Core Web API), routing, DI, and request handling

### ✨ Why Clean Architecture?

- Encourages **separation of concerns**
- Keeps business logic independent from frameworks
- Improves **testability** and **maintainability**
- Complies with **SOLID principles**

---

## 🌐 Richardson Maturity Model (RMM)

This API follows **Richardson Maturity Model Level 2**, which means:

| Level       | Description                                           | Applied?           |
| ----------- | ----------------------------------------------------- | ------------------ |
| Level 0     | One URI, one verb (POST everything)                   | ❌                 |
| Level 1     | Multiple URIs but not resource-based                  | ❌                 |
| **Level 2** | ✅ Proper use of HTTP verbs and resource-centric URIs | ✅                 |
| Level 3     | Hypermedia (HATEOAS)                                  | ❌ Not implemented |

### Why not Level 3 (HATEOAS)?

- The API is simple and internal-facing, so **hypermedia controls are not needed**
- HATEOAS is often reserved for **public or discoverable APIs**
- Adding HATEOAS would add complexity without functional gain in this scenario

---

## 🚀 How to Run the Application

1. **Clone the repo** and navigate to the project root:

```bash
git clone https://github.com/JanosPalinkas/TinyLedger.git
cd TinyLedger
```

2. **Build the solution**:

```bash
dotnet build
```

3. **Run the API**:

```bash
dotnet run --project TinyLedger.Api
```

4. **Access Swagger UI**:

> Open your browser and go to:  
> [http://localhost:5078/swagger](http://localhost:5078/swagger)

This gives you an interactive interface to explore and test the API.

---

## 🧪 Testing the API

You can test the API through:

- Swagger UI (recommended for manual tests)
- Any REST client like Postman or cURL
- Automated unit & integration tests

---

## 🔀 Available Endpoints

All endpoints are scoped under `api/accounts/{accountId}` for proper RESTful resource modeling.

| Method | Endpoint                                 | Description                 |
| ------ | ---------------------------------------- | --------------------------- |
| POST   | `/api/accounts/{accountId}/transactions` | Record a deposit/withdrawal |
| GET    | `/api/accounts/{accountId}/transactions` | Get transaction history     |
| GET    | `/api/accounts/{accountId}/balance`      | Get current balance         |

### 🔧 Example `POST` request body

```json
{
  "amount": 100.0,
  "type": "Deposit",
  "description": "Initial deposit"
}
```

---

## ✅ Tests

This project includes both **unit tests** and **integration tests** using xUnit and Moq.

### ▶️ Run All Tests

To run all tests with detailed output:

```bash
dotnet test TinyLedger.Tests --logger "console;verbosity=detailed"
```

This includes:

- Unit tests for core business logic
- Integration tests that call real API endpoints

To run only the unit tests:

```bash
dotnet test --filter "Category=UnitTests"
```

To run only the Integration tests:

```bash
dotnet test --filter "Category=Integration"
```

### 📊 Test Coverage

We use:

- `coverlet.collector` to collect code coverage
- `reportgenerator` to generate a readable HTML report

#### Generate Coverage Report

```bash
dotnet test TinyLedger.Tests --collect:"XPlat Code Coverage"
```

Then run:

```bash
reportgenerator \
  "-reports:TinyLedger.Tests/TestResults/**/coverage.cobertura.xml" \
  "-targetdir:coverage-report" \
  "-reporttypes:Html"
```

Then open the HTML report:

```bash
open coverage-report/index.html
```

---

### 🧪 Test Coverage Breakdown

| Use Case                        | Test File                                   |
| ------------------------------- | ------------------------------------------- |
| Record transaction (Command)    | `RecordTransactionCommandHandlerTests.cs`   |
| Get balance (Query)             | `GetBalanceQueryHandlerTests.cs`            |
| Get transaction history (Query) | `GetTransactionHistoryQueryHandlerTests.cs` |
| Full API integration            | `LedgerIntegrationTests.cs`                 |

---

## 📦 Technologies Used

- .NET 9
- ASP.NET Core Web API
- MediatR (CQRS & decoupling)
- Clean Architecture
- xUnit (unit & integration testing)
- Moq (mocking dependencies)
- Coverlet + ReportGenerator (code coverage)
- Swagger (OpenAPI documentation)

---

## 📌 License

This project is licensed under the MIT License.
