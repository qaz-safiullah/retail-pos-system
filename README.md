# RetailPOS - Point of Sale Web Application

A retail point-of-sale (POS) system built with **ASP.NET Core MVC 8.0** and **Entity Framework Core**, featuring role-based access for **Admin** and **Cashier** users.

## Features

### Authentication & Authorization
- Session-based login with SHA256 password hashing
- Two roles: `Admin` and `Cashier`
- Custom `[RoleAuthorize]` filter for action-level access control

### Admin Module
- Dashboard with sales stats, product/order counts, and low-stock alerts
- Full CRUD for **Products**, **Categories**, and **Users**
- Daily, monthly, and top-selling-products reports
- AJAX-based restock modal for quick inventory updates

### Cashier Module
- Personal dashboard showing today's orders and sales
- Point of Sale (POS) interface with:
  - Live product search (name or barcode)
  - Cart management with quantity controls
  - Cash/Card payment selection
  - Real-time change calculation
  - Transaction-safe order placement with stock validation

### Order Management
- Admin views all orders; Cashier views only their own
- Order detail with line items, payment info, and print button
- Print-friendly receipt page

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8.0 |
| Language | C# |
| Pattern | MVC |
| ORM | Entity Framework Core 8.0 |
| Database | Microsoft SQL Server |
| Frontend | Razor Views, Bootstrap 5, jQuery |
| Auth | Custom session-based |

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (Express or higher)
- Visual Studio 2022 (recommended)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/RetailPOS.git
cd RetailPOS
```

### 2. Database Setup

Run the schema script against your SQL Server instance:

```bash
sqlcmd -S YOUR_SERVER\SQLEXPRESS -i dbqueries.sql
```

Optionally seed test data (10 categories, 50 products):

```bash
sqlcmd -S YOUR_SERVER\SQLEXPRESS -i dbtesting.sql
```

### 3. Configure Connection String

Edit `RetailPOS/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER\\SQLEXPRESS;Database=POSDB;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### 4. Run the Application

```bash
cd RetailPOS
dotnet restore
dotnet run
```

The app will be available at `http://localhost:5097` (or `http://localhost:5000`).

### 5. Default Credentials

| Username | Password | Role |
|----------|----------|------|
| Admin | Admin | Admin |

The default admin user is auto-seeded on first run.

## Project Structure

```
RetailPOS/
├── RetailPOS.sln
├── dbqueries.sql              # Database schema
├── dbtesting.sql              # Test data
└── RetailPOS/                 # ASP.NET Core project
    ├── Program.cs             # Entry point, DI, middleware
    ├── appsettings.json       # Configuration
    ├── Models/                # Entity models & ViewModels
    ├── Data/
    │   └── POSDBContext.cs    # EF Core DbContext
    ├── Filters/
    │   └── RoleAuthorizeAttribute.cs
    ├── Controllers/           # 11 controllers
    ├── Views/                 # 33 Razor views
    └── wwwroot/               # Static files (CSS, JS, libs)
```

## Database Schema

Five tables: **Users**, **Categories**, **Products**, **Orders**, **OrderItems** with foreign key relationships and identity columns.

## License

This project is for educational purposes.
