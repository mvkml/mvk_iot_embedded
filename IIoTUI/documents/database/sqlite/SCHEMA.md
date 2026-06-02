# MariVshApp — SQLite Schema

**Owner:** Dev SQLite Agent  
**Reviewed By:** Architect Agent  
**Last Updated:** 2026-05-20  
**Phase:** 1 — SQLite (Active)

---

## Entity Relationship

```
UserType
   │
   │  TypeId (FK)
   ▼
UserAccount

ManageItem   (standalone — no FK relations in v1)
```

One `UserType` can have many `UserAccount` rows.  
`ManageItem` is a standalone table — represents navigation entries in the Manage page.

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

### ManageItem

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| Id | INTEGER | PK, AutoIncrement | Auto-generated SQLite row key |
| ItemId | INTEGER | | User-provided identifier — generic, maps to a future entity |
| Name | TEXT | | Display name — e.g. "Projects", "Components" |
| Description | TEXT | | Optional description |
| NavigateUrl | TEXT | | Shell route name for tap-to-navigate (e.g. `ProjectListPage`) |
| ImageUrl | TEXT | | Optional image or icon URL |
| IsActive | INTEGER | | 1 = active, 0 = inactive |
| CreatedDate | TEXT | | Set automatically on insert |
| UpdatedDate | TEXT | | Set automatically on insert and update |

**Notes:**
- `ManageItem` is the generic navigation/menu entry for the Manage page
- `ItemId` is user-provided and generic — intended to link to a future specific entity
- No FK constraints in v1 — relations will be added as hierarchy is built
- Managed by: `ManageItemRepository` → `ManageItemService` → `ManageViewModel` / `ItemViewModel`

---

## ComponentType

| Column | Type | Attributes | Notes |
|--------|------|------------|-------|
| TypeId | int | [PrimaryKey] | 1=Switch, 2=Monitor, 3=InfraRed, 4=Alarm |
| Name | string | | Display name |
| Description | string | | |
| IsActive | bool | | Default true |
| CreatedDate | DateTime | | |
| UpdatedDate | DateTime | | |

**Seeded on first run — not user-managed.**

---

## Known Gaps

| Gap | Severity | Fix |
|-----|----------|-----|
| Password stored plain text | 🔴 Critical | ADR 003 — PBKDF2 hashing |
| Admin routing not wired | 🟡 Medium | LoginViewModel reads UserTypeId and routes to AdminPage |
