# MariVshApp — Database Migration Plan

**Owner:** Dev SQLite Agent + Architect Agent  
**Last Updated:** 2026-05-17  
**Reference:** ADR 001 — Database Selection (`agile/architecture/decisions/ADR001_database_selection.md`)

---

## Overview

MariVshApp uses a 3-phase database strategy.  
Each phase uses a different database engine but the **same interfaces** — no ViewModel changes required.  
All changes are isolated to the `Repositories/` layer and `MauiProgram.cs` DI registration.

```
Phase 1 — SQLite       → mobile-first, offline, embedded
Phase 2 — PostgreSQL   → shared data, cloud backend, APIs
Phase 3 — SQL Server   → enterprise, Azure integration
```

---

## Phase 1 — SQLite (Current)

**Status:** Active  
**Timeline:** Learning phase — local development only

| Item | Detail |
|------|--------|
| Engine | SQLite via `sqlite-net-pcl` |
| Location | Device local storage (`FileSystem.AppDataDirectory`) |
| Schema | `UserAccount`, `UserType` — 2 tables |
| Connection | `SqliteConnectionProvider` (wraps `SQLiteAsyncConnection`) |
| Repositories | `Repositories/SQLite/UserAccountRepository.cs` |
| | `Repositories/SQLite/UserTypeRepository.cs` |
| Auth | Plain text passwords — ADR 003 (PBKDF2) will fix this |
| Data persistence | Tables dropped on each cold start (dev mode) |

**What to keep when migrating to Phase 2:**
- All interfaces: `IUserAccountRepository`, `IUserTypeRepository`
- All ViewModels — zero changes
- All Models (`UserAccount.cs`, `UserType.cs`) — same shape

**What changes:**
- Add `Repositories/PostgreSQL/` implementations
- Update `MauiProgram.cs` to register PostgreSQL repositories
- `Repositories/SQLite/` remains untouched (OCP — Open/Closed Principle)

---

## Phase 2 — PostgreSQL (Planned)

**Status:** Future — not yet started  
**Trigger:** When shared/multi-device data or cloud backend is needed

| Item | Detail |
|------|--------|
| Engine | PostgreSQL (via cloud backend or direct connection) |
| Location | Cloud-hosted DB (Azure, AWS, GCP — TBD) |
| New code | `Repositories/PostgreSQL/UserAccountRepository.cs` |
| | `Repositories/PostgreSQL/UserTypeRepository.cs` |
| Connection string | Stored in Azure Key Vault — NOT in code |
| DI registration | `MauiProgram.cs` swaps SQLite → PostgreSQL repositories |

**Secrets naming (Key Vault convention):**
```
marivshapp-dev-postgresql-connectionstring
marivshapp-staging-postgresql-connectionstring
marivshapp-prod-postgresql-connectionstring
```

**Migration tasks:**
- [ ] Create PostgreSQL schema (matches SQLite schema)
- [ ] Implement `Repositories/PostgreSQL/` with same interfaces
- [ ] Add Key Vault secret for connection string
- [ ] Update `MauiProgram.cs` registration
- [ ] Run integration tests against PostgreSQL instance
- [ ] ADR 004 — PostgreSQL selection and connection strategy

---

## Phase 3 — SQL Server (Planned)

**Status:** Future — enterprise readiness  
**Trigger:** Azure enterprise integration requirement

| Item | Detail |
|------|--------|
| Engine | Microsoft SQL Server (Azure SQL Database) |
| Location | Azure SQL Database |
| New code | `Repositories/SqlServer/UserAccountRepository.cs` |
| | `Repositories/SqlServer/UserTypeRepository.cs` |
| Connection string | Azure Key Vault |
| DI registration | `MauiProgram.cs` swaps to SqlServer repositories |

**Secrets naming (Key Vault convention):**
```
marivshapp-dev-sqlserver-connectionstring
marivshapp-staging-sqlserver-connectionstring
marivshapp-prod-sqlserver-connectionstring
```

**Migration tasks:**
- [ ] Create SQL Server schema (matches existing)
- [ ] Implement `Repositories/SqlServer/` with same interfaces
- [ ] Add Key Vault secret for connection string
- [ ] Update `MauiProgram.cs` registration
- [ ] LSP validation: SqlServer repos must fully substitute SQLite repos
- [ ] ADR 005 — SQL Server selection and Azure SQL strategy

---

## Interface Contract (unchanged across all phases)

Both `IUserAccountRepository` and `IUserTypeRepository` are defined once in `Repositories/Interfaces/`.  
Every phase must implement ALL methods — no optional methods (LSP compliance).

```csharp
// IUserAccountRepository.cs — all phases implement this
Task<UserAccount?> GetByUserIdAsync(string userId);
Task<int> InsertAsync(UserAccount account);
Task<bool> IsUserIdTakenAsync(string userId);
```

```csharp
// IUserTypeRepository.cs — all phases implement this
Task<List<UserType>> GetAllAsync();
Task<UserType?> GetByIdAsync(int typeId);
```

---

## Repository Folder Structure (target)

```
marivshapp/
└── Repositories/
    ├── Interfaces/
    │   ├── IUserAccountRepository.cs   ← defined once, used by all phases
    │   └── IUserTypeRepository.cs
    ├── SQLite/                          ← Phase 1 (active)
    │   ├── UserAccountRepository.cs
    │   └── UserTypeRepository.cs
    ├── PostgreSQL/                      ← Phase 2 (future)
    │   ├── UserAccountRepository.cs
    │   └── UserTypeRepository.cs
    └── SqlServer/                       ← Phase 3 (future)
        ├── UserAccountRepository.cs
        └── UserTypeRepository.cs
```

---

## DI Swap Pattern (MauiProgram.cs)

When migrating between phases, only ONE line changes per repository in `MauiProgram.cs`:

```csharp
// Phase 1 — SQLite
builder.Services.AddTransient<IUserAccountRepository, SQLite.UserAccountRepository>();

// Phase 2 — PostgreSQL (swap only this line)
builder.Services.AddTransient<IUserAccountRepository, PostgreSQL.UserAccountRepository>();

// Phase 3 — SQL Server (swap only this line)
builder.Services.AddTransient<IUserAccountRepository, SqlServer.UserAccountRepository>();
```

ViewModels inject `IUserAccountRepository` — they never know which phase is active.
