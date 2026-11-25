# WrenchFlow

Backend API for a vehicle repair workshop management system: customers and vehicles, job orders, faults, technician assignments, spare parts, invoices and payments.

> Training / graduation project. This repository contains the backend only; a frontend will consume the API.

## Tech Stack

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core 8 (Database First)
- SQL Server
- Swagger / OpenAPI

## Architecture

Three-layer architecture, one project per layer:

| Layer | Project | Responsibility |
|---|---|---|
| Presentation | `WrenchFlow.API` | Controllers, middleware, Swagger |
| Business | `WrenchFlow.BLL` | Services, DTOs, mapping, business rules |
| Data Access | `WrenchFlow.DAL` | Entities, `AppDbContext`, repositories |

Dependencies flow in one direction only: `API -> BLL -> DAL`.

Conventions:
- Controllers talk to Services through interfaces and use DTOs only. Entities never leave the BLL.
- Business rules live in the Services, not in Controllers.
- All data access and service methods are `async`.

## Project Structure

```
WrenchFlow
├── Database/              SQL script for WrenchFlowDB
├── WrenchFlow.API/        Controllers, Middlewares, Program.cs
├── WrenchFlow.BLL/        DTOs, Mapping, Services, Exceptions
└── WrenchFlow.DAL/        Entities, Data (AppDbContext), Repositories
```

## Database

SQL Server database `WrenchFlowDB` with 16 tables: People, Customers, Users, Specializations, UserSpecializations, Vehicles, JobOrders, JobOrderFaults, JobOrderFaultAssignments, SpareParts, JobOrderFaultSpareParts, Invoices, InvoiceItems, Payments, VehicleReleases, ActivityLogs.

A person (`People`) can be a customer, a user (employee/technician), or both.

## Getting Started

Prerequisites: Visual Studio 2022 (ASP.NET and web development workload), .NET 8 SDK, SQL Server (or Express) and SSMS.

1. Clone the repository.
2. Run `Database/WrenchFlowDB.sql` in SSMS to create the database.
3. Set your SQL Server name in `WrenchFlow.API/appsettings.json` (`ConnectionStrings:DefaultConnection`, the default is `Server=.`).
4. Run `WrenchFlow.API` and open `https://localhost:<port>/swagger`.

### Regenerating entities after a schema change

`Entities/` and `AppDbContext` are generated. Do not edit them by hand (use `partial` classes). From the Package Manager Console:

```
Scaffold-DbContext "Server=.;Database=WrenchFlowDB;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -ContextDir Data -Context AppDbContext -NoOnConfiguring -Force -Project WrenchFlow.DAL -StartupProject WrenchFlow.API
```

## API (current)

| Resource | Endpoints |
|---|---|
| Customers | `GET/POST /api/customers`, `GET/PUT/DELETE /api/customers/{id}` |
| Vehicles | `GET/POST /api/vehicles`, `GET/PUT /api/vehicles/{id}`, `GET /api/vehicles/by-customer/{customerId}` |

## Roadmap

- [x] Solution setup, entities, generic repository
- [x] Customers
- [x] Vehicles
- [ ] Specializations
- [ ] Users and authentication (JWT)
- [ ] Spare parts
- [ ] Job orders, faults, assignments
- [ ] Invoices and payments
- [ ] Vehicle release and activity logs
- [ ] CORS, authorization, final testing

## Workflow

- One branch per module (for example `feature/job-orders`) and a Pull Request into `main`.
- Do not commit secrets. Use user-secrets for the JWT key when authentication is added.
