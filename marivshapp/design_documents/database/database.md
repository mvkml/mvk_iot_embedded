# Database Design Document

## Overview
- **Database Engine:** SQLite
- **NuGet Package:** `sqlite-net-pcl` v1.9.172
- **Database File:** `marivshapp.db3`
- **Storage Location:** `FileSystem.AppDataDirectory` (app's local data folder)
- **Connection Manager:** `Database/DatabaseService.cs` (registered as singleton)

---

## Tables

### UserAccount

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| `Id` | `int` | Primary Key, Auto Increment | Unique identifier |
| `PhoneNumber` | `string` | — | User's phone number |
| `FirstName` | `string` | — | User's first name |
| `LastName` | `string` | — | User's last name |
| `Email` | `string` | — | User's email ID |
| `DateOfBirth` | `DateTime` | — | User's date of birth |
| `Gender` | `string` | — | Male or Female |
| `Username` | `string` | — | Login username |
| `Password` | `string` | — | Login password |

**Model File:** `Models/UserAccount.cs`

---

## Architecture

```
Database/
  DatabaseService.cs      → SQLite connection manager (singleton)

Models/
  UserAccount.cs          → User account table model

MauiProgram.cs            → Registers DatabaseService as singleton via DI
```

### DatabaseService
- Lazy initialization — database connection created on first access
- Automatically creates all required tables on first use
- Singleton pattern ensures one connection shared across the app
- Async API via `SQLiteAsyncConnection`

### Dependency Injection
```csharp
builder.Services.AddSingleton<DatabaseService>();
```

---

## Database File Path (Windows)
```
%LOCALAPPDATA%\Packages\<app-id>\LocalState\marivshapp.db3
```

---

## Status
- Database service: Created
- UserAccount table: Auto-created on first access
- Wired to Login/SignUp: Not yet — ready for next step
