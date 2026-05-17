# Sign Up — Technical Design Document

**Module:** UserAccount  
**Sub-Module:** Sign Up  
**Version:** v1  
**Date:** 2026-05-17  
**Status:** Active  

---

## Layer Architecture

```
┌─────────────────────────────────────────────────────┐
│  VIEW                                               │
│  Views/UserAccount/SignUpPage.xaml                  │
│  → User fills form, taps Sign Up button             │
└────────────────────┬────────────────────────────────┘
                     │ Binding / Command
┌────────────────────▼────────────────────────────────┐
│  VIEWMODEL                                          │
│  ViewModels/UserAccount/SignUpViewModel.cs           │
│  → Validates input, calls Business layer            │
└────────────────────┬────────────────────────────────┘
                     │ calls
┌────────────────────▼────────────────────────────────┐
│  BUSINESS LAYER (Service)                           │
│  Services/UserAccount/UserAccountService.cs         │
│  → Business rules: duplicate check, IsActive=true,  │
│    password handling (future: hashing)              │
└────────────────────┬────────────────────────────────┘
                     │ calls
┌────────────────────▼────────────────────────────────┐
│  REPOSITORY LAYER                                   │
│  Repositories/Interfaces/IUserAccountRepository.cs  │
│  Repositories/SQLite/UserAccountRepository.cs       │
│  → DB operations only: Insert, GetByUserId          │
└────────────────────┬────────────────────────────────┘
                     │ calls
┌────────────────────▼────────────────────────────────┐
│  DATABASE                                           │
│  Database/DatabaseService.cs                        │
│  → SQLite connection, table creation, seed data     │
└─────────────────────────────────────────────────────┘
```

---

## Design Patterns

| Pattern | Applied Where |
|---------|--------------|
| MVVM | `SignUpPage.xaml` ↔ `SignUpViewModel` via `INotifyPropertyChanged` + `ICommand` |
| Repository Pattern | `IUserAccountRepository` abstracts all DB operations from business logic |
| Service Layer Pattern | `UserAccountService` owns business rules — ViewModel never touches DB directly |
| Dependency Injection | All layers registered in `MauiProgram.cs` — constructor injection throughout |
| Singleton | `DatabaseService` — one SQLite connection shared across the app |
| Command Pattern | `SignUpCommand` bound to button — decouples UI action from logic |
| Observer Pattern | `INotifyPropertyChanged` — ViewModel notifies View of state changes |
| Interface Segregation | `IUserAccountRepository` and `IUserTypeRepository` are separate interfaces |

---

## Files

| Layer | File | Status |
|-------|------|--------|
| View | `Views/UserAccount/SignUpPage.xaml` | ✅ Exists |
| ViewModel | `ViewModels/UserAccount/SignUpViewModel.cs` | ✅ Exists |
| Service | `Services/UserAccount/UserAccountService.cs` | ❌ To implement |
| Interface | `Repositories/Interfaces/IUserAccountRepository.cs` | ❌ To implement |
| Repository | `Repositories/SQLite/UserAccountRepository.cs` | ❌ To implement |
| Database | `Database/DatabaseService.cs` | ✅ Exists |

---

## Dependency Injection Registration

```csharp
// MauiProgram.cs
builder.Services.AddSingleton<DatabaseService>();
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();
builder.Services.AddScoped<UserAccountService>();
builder.Services.AddTransient<SignUpViewModel>();
builder.Services.AddTransient<SignUpPage>();
```

---

## Interface Contract — IUserAccountRepository

```csharp
Task<UserAccount?> GetByUserIdAsync(string userId);
Task<int> InsertAsync(UserAccount user);
Task<int> UpdateAsync(UserAccount user);
Task<List<UserAccount>> GetAllAsync();
```

---

## Service Responsibilities — UserAccountService

| Method | Responsibility |
|--------|---------------|
| `SignUpAsync(...)` | Duplicate check → build UserAccount object → call repository Insert |
| `GetByUserIdAsync(...)` | Delegate to repository — used by LoginService |

---

## Data Flow — Sign Up

```
User taps Sign Up
    → SignUpViewModel.OnSignUpAsync()
        → Validates fields (ViewModel responsibility)
        → Calls UserAccountService.SignUpAsync(userId, name, password, description, userTypeId)
            → Calls IUserAccountRepository.GetByUserIdAsync(userId)
                → Returns existing record or null
            → If exists: return error result
            → If not: build UserAccount, call IUserAccountRepository.InsertAsync(user)
        → On success: show alert, clear form
```

---

## Open Items

| Item | Priority | Notes |
|------|----------|-------|
| Password hashing | 🔴 High | ADR 003 — PBKDF2 before any production use |
| Admin routing after login | 🟡 Medium | LoginViewModel reads UserTypeId, routes to AdminPage |
| UserTypeRepository + IUserTypeRepository | 🟡 Medium | Needed to load Account Type picker |
