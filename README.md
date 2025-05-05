# SmallBiz Manager

**SmallBiz Manager** is a comprehensive management system built with ASP.NET Core MVC and Web API to manage a small retail business (e.g., a boutique or electronics store). The system includes modules for products, employees, orders, and secure user authentication.

---

## 🚀 Tech Stack

- **ASP.NET Core MVC** (UI & Views)
- **ASP.NET Core Web API** (Backend services)
- **Entity Framework Core** (ORM)
- **SQL Server / SQLite** (Relational database)
- **Razor Views** (Dynamic HTML rendering)
- **Bootstrap** (UI styling)
- Export to excel
---

## 📦 Features

### 🔐 Authentication & Security
- User Registration with **Admin** and **Staff** roles
- Secure login & logout
- **Cookie-based authentication**
- **Role-based access** (e.g., Admin-only pages)
- Passwords are **hashed securely** using ASP.NET Identity standards

### 📦 Product Management
- Add, update, delete products
- Upload **main product image** and auto-generate **thumbnail**
- Display product listings on shop UI
- Fields: Name, Price, Description, Image

### 👥 Employee Management
- Admin-only access
- Add, edit, remove employees
- Assign roles (Sales, Manager, etc.)

### 🧾 Customer Order Management
- Order creation by selecting products and quantity
- View order list with status filter (Pending, Shipped, Completed)
- Assign orders to employees
- Export order list to Excel

### 📊 Dashboard
- Summary of total products, orders, employees
- Monthly sales overview (displayed via simple chart)

---

## 📡 API Endpoints

| Method | Endpoint                | Description                   | Role        |
|--------|-------------------------|-------------------------------|-------------|
| GET    | /api/products           | List all products             | Public      |
| POST   | /api/products           | Add new product               | Admin       |
| PUT    | /api/products/{id}      | Update product                | Admin       |
| DELETE | /api/products/{id}      | Delete product                | Admin       |
| GET    | /api/orders             | List all orders               | Admin       |
| POST   | /api/orders             | Create a new order            | Staff/Admin |
| GET    | /api/employees          | List all employees            | Admin       |

---

## 🛠️ Database Design

- Fully **normalized** relational schema
- Tables:
  - `Users` (Admin/Staff, hashed passwords)
  - `Products` (Name, Price, ImagePath, Stock)
  - `Employees` (Name, Role, Linked to orders)
  - `Orders` (CustomerName, Status, OrderDate)
  - `OrderItems` (OrderId, ProductId, Quantity, Price)

- Created using **Entity Framework Core** code-first approach with migrations

---

## 🔐 Security Features

- Role-based policy filters using `[Authorize(Roles = "Admin")]`
- Hashed passwords using built-in Identity framework
- **Cookie authentication** with secure session management
- Input validation and anti-forgery tokens in Razor forms
- API protection by role checks

---

## 🧪 Test Data

- Included with database seed method or SQL script:
  - 3 sample products
  - 2 sample employees
  - 2 test users: **admin@demo.com** (Admin), **staff@demo.com** (Staff)
  - Sample orders for dashboard testing

---

## 📄 Optional Features

- 🟨 [Optional] Export to Excel (can be extended)

---

## ✅ Getting Started

### Prerequisites
- .NET 6 or later
- SQL Server or SQLite
- Visual Studio 2022+

### Setup Steps

1. Clone the repo:

https://github.com/abhijit737/SmallBizManager

2. Update DB connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=SmallBizDb;TrustServerCertificate=True;Trusted_Connection=True;"
}

3. Apply EF migrations and create the database:

	dotnet ef database update


4. Run the project:
		
		dotnet run


SmallBizManager/
│
├── Controllers/
│   ├── AccountController.cs
│   ├── ProductController.cs
│   ├── OrderController.cs
│   ├── EmployeeController.cs
│   ├── ProductController.cs
│
├── Models/
│   ├── Product.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   ├── Employee.cs
│   ├── OrderItemInputModel.cs
│   ├── RegisterViewModel.cs
│   ├── LoginViewModel.cs
│   ├── DashboardMetrics.cs
│   ├── CreateOrderViewModel.cs
│   ├── ErrorViewModel.cs
│   ├── Auth/
│       ├── RegisterRequest.cs
│       ├── LoginRequest.cs
│       ├── AuthResponse.cs
│
├── Services/
│   ├── ProductService.cs
│   ├── IProductService.cs
│   ├── OrderService.cs
│   ├── IOrderService.cs
│   ├── EmployeeService.cs
│   ├── IEmployeeService.cs
│   ├── AuthService.cs
│   ├── IAuthService.cs
│
├── Views/
│   ├── Account/
│   ├── Product/
│   ├── Order/
│   ├── Employee/
│   ├── Home/
│   ├── Shared/
│
├── wwwroot/
│   ├── css/
│   ├── images/
│
├── appsettings.json
├── Startup.cs / Program.cs
└── README.md



Please find attched UI Images-

 # Please Check ProjectImages Folder Files for reference.



> ![Register Page](https://raw.githubusercontent.com/abhijit737/SmallBizManager/blob/master/ProjectImages/smallbizregister.png?raw=true)

> ![Login Page](https://raw.githubusercontent.com/abhijit737/SmallBizManager/blob/master/ProjectImages/smallbizlogin.png?raw=true)

> ![Home Page](https://raw.githubusercontent.com/abhijit737/SmallBizManager/blob/master/ProjectImages/smallbizhome.png?raw=true)

> ![Producs Page](https://raw.githubusercontent.com/abhijit737/SmallBizManager/blob/master/ProjectImages/smallbizproductsindex.png?raw=true)

> ![Product Edit Page](https://raw.githubusercontent.com/abhijit737/SmallBizManager/blob/master/ProjectImages/smallbizproductedit.png?raw=true)

> ![Employee Page](https://raw.githubusercontent.com/abhijit737/SmallBizManager/blob/master/ProjectImages/smallbizemployeesindex.png?raw=true)

> ![Employee Create Page](https://raw.githubusercontent.com/abhijit737/SmallBizManager/blob/master/ProjectImages/smallbizcreateemployee.png?raw=true)

> ![Employee Delete Page](https://raw.githubusercontent.com/abhijit737/SmallBizManager/blob/master/ProjectImages/smallbizDeleteemployee.png?raw=true)

> ![Orders Page](https://raw.githubusercontent.com/abhijit737/SmallBizManager/blob/master/ProjectImages/smallbizOrders.png?raw=true)

> ![Create Order Page](https://raw.githubusercontent.com/abhijit737/SmallBizManager/blob/master/ProjectImages/smallbizCreateOrder.png?raw=true)

> ![Edit Order Page](https://raw.githubusercontent.com/abhijit737/SmallBizManager/blob/master/ProjectImages/smallbizEditOrder.png?raw=true)

> ![Delete Order Page](https://raw.githubusercontent.com/abhijit737/SmallBizManager/blob/master/ProjectImages/smallbizdeleteOrder.png?raw=true)









