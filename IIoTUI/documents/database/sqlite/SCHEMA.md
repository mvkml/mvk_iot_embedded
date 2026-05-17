# MariVshApp — SQLite Schema

**Owner:** Dev SQLite Agent  
**Reviewed By:** Architect Agent  
**Last Updated:** 2026-05-17  
**Phase:** 1 — SQLite (Active)

---

## Entity Relationship

```
UserType
   │
   │  TypeId (FK)
   ▼
UserAccount
```

One `UserType` can have many `UserAccount` rows.  
Every `UserAccount` must have a valid `TypeId`.

---

## Tables

### UserType

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| TypeId | INTEGER | PK, AutoIncrement | Unique type identifier |
| TypeName | TEXT | NOT NULL | Display name of the user type |

**Seed Data (inserted on every cold start — dev mode):**

| TypeId | TypeName |
|--------|----------|
| 1 | User |
| 2 | Admin |

**Notes:**
- `UserType.ToString()` returns `TypeName` — used by MAUI Picker binding
- Managed by: `DatabaseService.cs` (seed) → `UserTypeRepository` (queries)

---

### UserAccount

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| Id | INTEGER | PK, AutoIncrement | Internal row identifier |
| UserId | TEXT | UNIQUE | Login identifier — phone number or email |
| Name | TEXT | | Display name |
| Password | TEXT | | ⚠️ Plain text — ADR 003 (PBKDF2) pending |
| Description | TEXT | | Optional user description |
| IsActive | INTEGER | | 1 = active, 0 = inactive |
| CreatedDate | TEXT | | Row creation timestamp |
| UpdatedDate | TEXT | | Last update timestamp |
| UserTypeId | INTEGER | FK → UserType.TypeId | Determines User vs Admin routing |

**Notes:**
- `UserId` is enforced UNIQUE at DB level via `[Unique]` attribute
- `Password` is plain text — flagged as **security blocker** — ADR 003 pending
- `UserTypeId = 1` → navigates to HomePage on login
- `UserTypeId = 2` → should navigate to AdminPage — gap: not yet wired in `LoginViewModel`

---

## Runtime File Location

```
Platform     Path
─────────    ──────────────────────────────────────────────────────────────────
Windows      %LOCALAPPDATA%\<AppName>\com.companyname.marivshapp\Data\marivshapp.db3
Android      /data/data/com.companyname.marivshapp/files/marivshapp.db3
iOS/macOS    <app-sandbox>/Library/marivshapp.db3
```

Resolved in code via:
```csharp
Path.Combine(FileSystem.AppDataDirectory, "marivshapp.db3")
```

> Runtime-only file — auto-created by SQLite on first launch.  
> NOT a code file. NOT in source control.

---

## Dev Mode Behaviour

`DatabaseService.cs` drops and recreates tables on every cold start:
```csharp
await _database.DropTableAsync<UserAccount>();
await _database.DropTableAsync<UserType>();
await _database.CreateTableAsync<UserAccount>();
await _database.CreateTableAsync<UserType>();
// seed UserType rows
```

All data is lost on every app restart during development.  
Persistence will be enabled when moving to staging/production.

---

## Architecture

```
Database/
  DatabaseService.cs      → SQLite connection manager (singleton)

Models/
  UserAccount.cs          → UserAccount table model
  UserType.cs             → UserType table model

MauiProgram.cs            → Registers DatabaseService as singleton via DI
```

### DatabaseService
- Lazy initialization — connection created on first access
- Automatically creates all tables on first launch
- Singleton pattern — one connection shared across the app
- Async API via `SQLiteAsyncConnection`

### Dependency Injection
```csharp
builder.Services.AddSingleton<DatabaseService>();
```

---

## Known Gaps

| Gap | Severity | Fix |
|-----|----------|-----|
| Password stored plain text | 🔴 Critical | ADR 003 — PBKDF2 hashing |
| Admin routing not wired | 🟡 Medium | LoginViewModel reads UserTypeId and routes to AdminPage |
