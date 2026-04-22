# ASP.NET MVC CRUD App — Project Plan
**Technology:** ASP.NET MVC (.NET Framework 4.8) — Legacy  
**IDE:** Visual Studio 2022 Community  
**Database:** SQL Server Express (DESKTOP-N74VGJ2\SQLEXPRESS)  
**Entity:** Student Management  

---

## What We Are Building
A simple web application to **Create, Read, Update, and Delete** student records.

### Student Fields
| Field  | Type   | Notes                        |
|--------|--------|------------------------------|
| Id     | int    | Auto-generated, Primary Key  |
| Name   | string | Student full name            |
| Age    | int    | Student age                  |
| Email  | string | Student email                |
| Grade  | string | e.g. A, B, C, D             |

---

## Tech Stack
| Layer    | Technology                               |
|----------|------------------------------------------|
| Frontend | Razor Views (.cshtml) + Bootstrap        |
| Backend  | ASP.NET MVC Controllers (C#)             |
| ORM      | Entity Framework 6 (Code First)          |
| Database | SQL Server Express (DESKTOP-N74VGJ2\SQLEXPRESS) |

---

## Key Concepts Learned So Far

### Code First vs Database First
- **Code First (our approach):** Write C# classes first, EF creates the database tables automatically
- **Database First:** Design tables in SQL Server first, EF generates C# classes from them
- Modern projects prefer Code First. Database First is used when a database already exists.

### What is Entity Framework?
- A library that acts as a bridge between C# code and the database
- Translates C# classes into SQL tables automatically
- We never write raw SQL — EF handles it

### What is ApplicationDbContext?
- The "door" to our database
- Tells EF which tables to create (via DbSet properties)
- Holds the connection string name to find the database

### What are Migrations?
- Think of migrations as **version control for your database** (like Git for code)
- Every time you change a Model class, you create a new migration
- EF tracks all changes in order so the database always stays in sync with your code

#### The 3 Migration Commands Explained:
| Command | What it does | Real world analogy |
|---|---|---|
| `Enable-Migrations` | Activates migration tracking for the project (run once only) | Hiring an architect |
| `Add-Migration <n>` | Takes a snapshot of your models and writes SQL instructions in C# | Drawing the blueprint |
| `Update-Database` | Actually runs the SQL and creates/updates the real database | Construction workers building it |

#### Useful Extra Migration Commands:
| Command | What it does |
|---|---|
| `Update-Database -Verbose` | Shows exactly which server EF is connecting to and what SQL it runs |
| `Update-Database -TargetMigration:0` | Rolls back ALL migrations (wipes DB history) |
| `Update-Database -Force` | Forces re-run even if migration seems applied |

### What is a Connection String?
- Tells the app WHERE to find the database and WHAT to call it
- Stored in `Web.config` so it can be changed without rewriting code
- Must be placed **right after** `</configSections>` in `Web.config` — XML is order-sensitive
- Our final working connection string:
```xml
<connectionStrings>
    <add name="DefaultConnection"
         connectionString="Server=DESKTOP-N74VGJ2\SQLEXPRESS;Database=CrudAppDb;Trusted_Connection=True;TrustServerCertificate=True;"
         providerName="System.Data.SqlClient" />
</connectionStrings>
```

### ⚠️ Important Gotchas Learned
| Problem | Cause | Fix |
|---|---|---|
| EF used LocalDB instead of SQL Server | `<connectionStrings>` was placed at bottom of `Web.config` | Move it to right after `</configSections>` |
| SSL certificate error when connecting | SQL Server 2022+ enforces strict encryption by default | Add `TrustServerCertificate=True` to connection string |
| "No pending migrations" after fix | EF cached old migration history | Use `Update-Database -TargetMigration:0` then `Update-Database` again |
| `localhost` vs machine name | `localhost` sometimes doesn't resolve correctly | Use exact machine name `DESKTOP-N74VGJ2\SQLEXPRESS` |

### What is a Migration File?
- Auto-generated C# file inside the `Migrations` folder
- Contains `Up()` method — SQL instructions to apply the change (create table)
- Contains `Down()` method — SQL instructions to reverse the change (drop table)
- Named with a timestamp + your chosen name e.g. `202604201331591_InitialCreate.cs`

---

## Step-by-Step Plan

### PHASE 1 — Setup & Configuration
- [x] Step 1: Verify project runs (MVC default page) ✅
- [x] Step 2: Install SQL Server Express ✅
- [x] Step 3: Install Entity Framework 6 via NuGet ✅

### PHASE 2 — Database & Model
- [x] Step 4: Create the `Student` Model class ✅
- [x] Step 5: Create the `ApplicationDbContext` class ✅
- [x] Step 6: Configure connection string in `Web.config` ✅
- [x] Step 7: Run Entity Framework migrations (create the DB table) ✅

### PHASE 3 — CRUD Operations (Controller)
- [ ] Step 8: Create `StudentsController` ⬅️ YOU ARE HERE
- [ ] Step 9: Implement `Index` action — Read (list all students)
- [ ] Step 10: Implement `Create` action — Create (add new student)
- [ ] Step 11: Implement `Edit` action — Update (edit student)
- [ ] Step 12: Implement `Delete` action — Delete (remove student)

### PHASE 4 — Views (UI Pages)
- [ ] Step 13: Create `Index.cshtml` — list all students (table)
- [ ] Step 14: Create `Create.cshtml` — form to add a student
- [ ] Step 15: Create `Edit.cshtml` — form to edit a student
- [ ] Step 16: Create `Delete.cshtml` — confirmation page

### PHASE 5 — Testing & Polish
- [ ] Step 17: Test all CRUD operations end to end
- [ ] Step 18: Add basic validation (required fields, email format)
- [ ] Step 19: Add navigation links between pages
- [ ] Step 20: Final review and cleanup

---

## Key Concepts You Will Learn
- [x] How MVC pattern works (Model → Controller → View)
- [x] Code First vs Database First approach
- [x] What Entity Framework is and how it works
- [x] What Migrations are and why we need them
- [x] How connection strings work and where to place them
- [x] What a Migration file contains (Up/Down methods)
- [x] How SQL Server 2022+ handles encryption (TrustServerCertificate)
- [ ] How Razor syntax works (.cshtml files)
- [ ] How HTML forms submit data to a Controller
- [ ] How SQL Server stores and retrieves data

---

## Progress Tracker
**Current Status:** Phase 3 — CRUD Operations  
**Last Completed Step:** Step 7 — Migrations ran successfully, CrudAppDb and Students table verified in SQL Server Express ✅  
**Next Step:** Step 8 — Create StudentsController