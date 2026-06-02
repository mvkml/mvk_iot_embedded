# MariVshApp — Claude Code Context

This file is auto-loaded at the start of every Claude Code session for this project.
Keep it up to date as the project evolves.

---

## What This App Is

**MariVshApp** is an on-premise IoT management and control desktop app (Windows, .NET MAUI 9).
Built for configuring and controlling custom hardware — ESP32-based Hubs and Components.

**Hardware architecture:**
```
App (this) → Hub (ESP32 + SD + RTC) → Component/Node (ESP32 + Relay) → Physical Device
```

**Data hierarchy:**
```
Project → Hub → Room → Component/Node → Device (fan, light, TV, fish tank...)
```

**Real example:** A 2BHK house is one Project. Hall room has Fan, 2× Light, Tube Light, TV, Fish Tank — each is one Component.

**Developer:** Vishnu (mvkwithmath@gmail.com)

---

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Framework | .NET MAUI 9 |
| Language | C# |
| UI | XAML |
| Pattern | MVVM + Repository + Service Layer |
| Local DB | SQLite via `sqlite-net-pcl` |
| DI | `Microsoft.Extensions.DependencyInjection` |
| Navigation | Shell navigation (`GoToAsync`) |
| Platform | Windows (unpackaged), Android/iOS future |

**DB path (stable, survives clean/rebuild):**
```
%LOCALAPPDATA%\MariVshApp\marivshapp.db3
```
i.e. `C:\Users\<user>\AppData\Local\MariVshApp\marivshapp.db3`

---

## Project Folder Structure

```
IIoTUI\
├── CLAUDE.md                          ← this file
├── marivshapp\                        ← .NET MAUI app project
│   ├── Models\
│   │   ├── UserAccount.cs
│   │   └── UserType.cs
│   ├── Database\
│   │   └── DatabaseService.cs         ← Singleton; eager init on startup
│   ├── Repositories\
│   │   ├── Interfaces\
│   │   │   ├── IUserAccountRepository.cs
│   │   │   └── IUserTypeRepository.cs
│   │   └── SQLite\
│   │       ├── UserAccountRepository.cs
│   │       └── UserTypeRepository.cs
│   ├── Services\
│   │   └── UserAccount\
│   │       └── UserAccountService.cs  ← all business logic
│   ├── ViewModels\
│   │   ├── UserAccount\
│   │   │   ├── LoginViewModel.cs
│   │   │   └── SignUpViewModel.cs
│   │   ├── Home\
│   │   │   └── HomeViewModel.cs
│   │   └── Admin\
│   │       └── AdminViewModel.cs
│   ├── Views\
│   │   ├── UserAccount\
│   │   │   ├── LoginPage.xaml
│   │   │   └── SignUpPage.xaml
│   │   ├── Home\
│   │   │   └── HomePage.xaml
│   │   └── Admin\
│   │       └── AdminPage.xaml
│   ├── AppShell.xaml(.cs)
│   └── MauiProgram.cs
└── documents\
    ├── llm\                           ← versioned CLAUDE.md snapshots (CLAUDE_v1.md, v2, ...)
    ├── tools\                         ← VS Code extensions, scripts
    ├── modules\
    │   └── UserAccount\
    │       ├── Login\  (FDD.md, TDD.md)
    │       └── SignUp\ (FDD.md, TDD.md, SignUp_Flowchart.md)
    ├── database\
    │   └── sqlite\    (SCHEMA.md, CHANGELOG.md, schemas/)
    └── artifacts\
        └── database\sqlite\ (versioned .db3 backups, backup_db.ps1)
```

---

## DI Registration (MauiProgram.cs)

```csharp
builder.Services.AddSingleton<DatabaseService>();
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();
builder.Services.AddScoped<UserAccountService>();
builder.Services.AddTransient<LoginViewModel>();
builder.Services.AddTransient<LoginPage>();
builder.Services.AddTransient<SignUpViewModel>();
builder.Services.AddTransient<SignUpPage>();
builder.Services.AddTransient<HomeViewModel>();
builder.Services.AddTransient<HomePage>();
builder.Services.AddTransient<AdminViewModel>();
builder.Services.AddTransient<AdminPage>();
```

---

## Shell Navigation (AppShell.xaml.cs)

- **Shell root:** `LoginPage` (always the first page)
- **Registered routes:** `SignUpPage`, `HomePage`, `AdminPage`
- Navigation uses `Shell.Current.GoToAsync(nameof(Page))` or `".."`

---

## DB Tables (Current)

### UserType
| Column | Type | Notes |
|--------|------|-------|
| TypeId | int (PK) | 1=User, 2=Admin |
| TypeName | string | |
| Description | string | |
| IsActive | bool | |
| CreatedDateTime | DateTime | |
| UpdatedDateTime | DateTime | |

Seeded on first run: TypeId 1 (User), TypeId 2 (Admin).

### UserAccount
| Column | Type | Notes |
|--------|------|-------|
| Id | int (PK, auto) | |
| UserId | string | Unique — phone or email |
| Name | string | Display name |
| Password | string | Plain text — ADR 003 PBKDF2 pending |
| Pin | string | 4-digit — unique across all users |
| Description | string | Optional |
| IsActive | bool | |
| UserTypeId | int (FK) | → UserType.TypeId |
| CreatedDate | DateTime | |
| UpdatedDate | DateTime | |

---

## Key Conventions

- **Namespace collision rule:** `UserAccountService` lives in `MariVshApp.Services.UserAccount`. Inside that file, always use `Models.UserAccount` (not just `UserAccount`) to avoid CS0118 namespace-vs-type conflict.
- **DB init:** `DatabaseService` initialises eagerly in constructor via `Task.Run`. No need to navigate to trigger DB creation.
- `CreateTableAsync` uses `CREATE TABLE IF NOT EXISTS` — safe, never drops data.
- **PIN:** 4-digit string, stored as-is, must be unique across all users.

---

## Current State (as of 2026-05-18)

### Done
- Login page: User ID + Password login, 4-digit PIN login
- Sign Up page: all fields including PIN, "Go to Login →" link on success
- UserAccount and UserType tables with seeded data
- DB at stable `%LOCALAPPDATA%` path
- Repository and Service layers fully wired
- HomePage and AdminPage stubs exist

### Pending / Open Items
| Item | Priority |
|------|----------|
| Admin routing: LoginViewModel must check `UserTypeId` and route TypeId=2 to `AdminPage` | 🔴 High |
| Password hashing (ADR 003 — PBKDF2) | 🔴 High |
| HomePage layout: Header + Left Sidebar + Content Area + Footer | 🟡 Medium |
| Left sidebar links: Favourites (default), Projects, Manage, User Info, Admin (admin only) | 🟡 Medium |
| Favourites page content spec | 🟡 Medium |
| Schema docs: add Pin column to UserAccount_v1.md or create v2 | 🟢 Low |
| artifacts README: add marivshapp_v2.db3 entry | 🟢 Low |
| `DatabaseService.GetActiveUserTypesAsync()` unused — can be removed | 🟢 Low |

---

## DB Backup

To archive the live DB:
```powershell
.\documents\artifacts\database\sqlite\backup_db.ps1 -Version <N>
```
Source: `%LOCALAPPDATA%\MariVshApp\marivshapp.db3`
Dest: `documents\artifacts\database\sqlite\marivshapp_v<N>.db3`

Current snapshots: v1 (2026-05-17), v2 (2026-05-18)

---

## Design Documents

| Module | FDD | TDD | Flowchart |
|--------|-----|-----|-----------|
| SignUp | `documents/modules/UserAccount/SignUp/FDD.md` | `TDD.md` | `SignUp_Flowchart.md` |
| Login | `documents/modules/UserAccount/Login/FDD.md` | `TDD.md` | — |
| Home | `documents/modules/Home/HomePage_Plan.md` | — | — |
