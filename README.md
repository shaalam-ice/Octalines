# Octalines — Business Management Application

A complete ABP Framework (ASP.NET Core) business management web application with POS, Inventory, Accounts, and CRM modules, built with a clean modular architecture.

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Framework | ABP Framework 10.x |
| Backend | ASP.NET Core (.NET 10) |
| ORM | Entity Framework Core + SQL Server |
| UI | ASP.NET Core MVC Razor Pages |
| Auth | ABP Identity (cookie authentication) |
| Localization | English |

## Architecture

```
src/
├── Octalines.Domain.Shared          # Enums, constants
├── Octalines.Domain                  # Entities, domain logic
├── Octalines.Application.Contracts  # DTOs, IAppService interfaces, Permissions
├── Octalines.Application             # App service implementations, AutoMapper
├── Octalines.EntityFrameworkCore    # DbContext, EF config, migrations, seed data
├── Octalines.HttpApi                 # REST API controllers
├── Octalines.Web                     # Razor Pages UI
└── Octalines.DbMigrator              # Migration runner CLI
```

## Prerequisites

- .NET 10 SDK
- SQL Server (or SQL Server LocalDB)
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

## Setup

### 1. Configure Connection String

Edit `src/Octalines.Web/appsettings.json` and `src/Octalines.DbMigrator/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "Default": "Server=YOUR_SERVER;Database=Octalines;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

For LocalDB:
```json
"Default": "Server=(localdb)\\MSSQLLocalDB;Database=Octalines;Trusted_Connection=True"
```

### 2. Run Database Migrations

```bash
cd src/Octalines.DbMigrator
dotnet run
```

This will:
- Create the database schema (all tables)
- Seed roles: Admin, Cashier, Manager
- Seed sample data: categories, units, taxes, warehouses, items, accounts, store settings

### 3. Run the Application

```bash
cd src/Octalines.Web
dotnet run
```

Navigate to `https://localhost:5001` (or the port shown in the console).

### Default Admin Credentials

After seeding, use ABP's built-in admin account:

| Field    | Value      |
|----------|-----------|
| Username | `admin`    |
| Password | `1q2w3E*`  |

> **Note:** These are ABP's default credentials. Change the password after first login.

## Modules & Features

### 1. Dashboard
- Summary cards: Today Sales, Month Sales, Customers, Suppliers, Low Stock, Cash Balance
- Sales trend chart (last 30 days)
- Top 10 items by revenue

### 2. POS (Point of Sale)
- Fast item search by name or barcode (keyboard-friendly)
- Cart with quantity editing
- Customer selection, warehouse selection
- Discount application
- Multiple payment methods (Cash, Card, Mobile Wallet)
- Change calculation
- Receipt display and print

### 3. Sales
- Sales list with filtering by invoice number, date, customer
- Sale detail view with line items and payments
- Add payment to existing invoices
- Sales returns with stock reversal

### 4. Purchases
- Purchase orders list
- Create new purchases with supplier selection
- Purchase payments

### 5. Contacts
- Customers CRUD (Create/Read/Update/Delete)
- Suppliers CRUD
- Search by name/phone

### 6. Inventory
- **Items:** Product & Service CRUD with categories, cost/sale price, stock tracking
- **Stock Adjustments:** Increase/decrease stock with audit trail
- **Stock Transfers:** Move stock between warehouses
- **Stock Ledger:** Immutable log of all stock movements
- **Warehouses:** Manage multiple warehouses

### 7. Accounts
- Cash, Bank, Mobile Wallet accounts
- Money transfers between accounts
- Deposits
- Balance tracking

### 8. Expenses
- Expense tracking with categories
- Account deduction

### 9. Coupons
- Global coupons (percentage or fixed discount)
- Customer-specific coupons

### 10. Quotations
- Create quotations
- Convert quotation to sale

### 11. Advance Payments
- Track advance payments from customers / to suppliers

### 12. Messaging
- Send SMS/WhatsApp messages (mock provider — logs messages)
- Message templates

### 13. Reports
- Report catalog page (P&L, Sales, Purchases, Stock, Expenses, and more)

### 14. Settings
- Store name, address, phone, currency
- Negative stock control
- SMS/API provider keys

## API Endpoints

The application exposes a REST API at `/api/app/`:

| Module | Endpoints |
|--------|----------|
| Contacts | `GET/POST/PUT/DELETE /api/app/contacts` |
| Items | `GET/POST/PUT/DELETE /api/app/items`, `GET /api/app/items/search` |
| Sales | `GET/POST /api/app/sales`, `POST /api/app/sales/payment`, `POST /api/app/sales/returns` |
| Purchases | `GET/POST /api/app/purchases` |
| Accounts | `GET/POST/PUT/DELETE /api/app/accounts`, `POST /api/app/accounts/transfer`, `POST /api/app/accounts/deposit` |
| Warehouses | `GET/POST/PUT/DELETE /api/app/warehouses`, `GET /api/app/warehouses/all` |
| Stock | `GET/POST /api/app/stock/adjustments`, `/transfers`, `/ledger` |
| Expenses | `GET/POST/PUT/DELETE /api/app/expenses` |
| Coupons | `GET/POST/PUT/DELETE /api/app/coupons`, `/customer-coupons` |
| Quotations | `GET/POST /api/app/quotations`, `POST /api/app/quotations/{id}/convert-to-sale` |
| Dashboard | `GET /api/app/dashboard/summary` |
| Settings | `GET/PUT /api/app/settings` |
| Messaging | `POST /api/app/messaging/send`, `GET/POST/PUT/DELETE /api/app/messaging/templates` |

## Domain Rules

- Every stock change writes to `StockLedger` (immutable audit trail)
- Sale creation checks stock availability (unless "Allow Negative Stock" is ON)
- Soft delete on master data (Customers, Suppliers, Items, etc.)
- Full audit log (created by/at, modified by/at) via ABP

## Permissions

Each module has granular permissions:

```
Octalines.Sales.View / Create / Edit / Delete
Octalines.Purchases.View / Create / Edit / Delete
Octalines.Items.View / Create / Edit / Delete
Octalines.Customers.View / Create / Edit / Delete
Octalines.Accounts.View / Create / Edit / Delete
Octalines.Expenses.View / Create / Edit / Delete
Octalines.Stock.View / Create
Octalines.Coupons.View / Create / Edit / Delete
Octalines.Quotations.View / Create / Delete
Octalines.Messaging.View / Send
Octalines.Settings.View
Octalines.Dashboard.View
Octalines.Warehouses.View / Create / Edit / Delete
```

## Re-creating Migrations (if needed)

```bash
# Remove and recreate
dotnet ef migrations remove \
    --project src/Octalines.EntityFrameworkCore \
    --startup-project src/Octalines.EntityFrameworkCore

dotnet ef migrations add InitialCreate \
    --project src/Octalines.EntityFrameworkCore \
    --startup-project src/Octalines.EntityFrameworkCore \
    --output-dir Migrations \
    --context OctalinesDbContext
```

## Project Structure Details

### Domain Layer
- All entities extend `FullAuditedAggregateRoot<Guid>` (with soft delete + audit)
- `StockLedger` extends `AuditedEntity<Guid>` (no soft delete — immutable log)
- Enums are in `Octalines.Domain.Shared/Enums.cs`

### Application Layer
- App services use `IRepository<T, Guid>` from ABP for all data access
- AutoMapper profile: `OctalinesApplicationAutoMapperProfile`
- All app services use `[Authorize(OctalinesPermissions.X.Default)]`

### EF Core Layer
- All tables prefixed with `OctlXxx` to avoid name conflicts
- `OctalinesDbContext` implements all required ABP interfaces
- Seed data in `OctalinesDbContextSeedContributor`

## Development Notes

- The application uses ABP Framework's conventional controllers — app services are automatically exposed as API endpoints
- All entities use `Guid` primary keys generated by `IGuidGenerator`
- The POS page uses a separate layout (`_POSLayout.cshtml`) optimized for fast entry
