# Sign Up — Action Plan

**Module:** UserAccount / Sign Up  
**Scope:** Create operation only (CRUD — C phase)  
**Date:** 2026-05-17  
**Status:** Pending implementation  

> ⚠️ Temporary document. Delete once all steps are marked ✅ Done.

---

## Now — Create (Sign Up)

| # | Step | File | Status |
|---|------|------|--------|
| 1 | Define repository interface | `Repositories/Interfaces/IUserAccountRepository.cs` | ⏳ Pending |
| 2 | Implement repository (Create + GetByUserId) | `Repositories/SQLite/UserAccountRepository.cs` | ⏳ Pending |
| 3 | Implement service (SignUpAsync) | `Services/UserAccount/UserAccountService.cs` | ⏳ Pending |
| 4 | Wire ViewModel to service (remove direct DB call) | `ViewModels/UserAccount/SignUpViewModel.cs` | ⏳ Pending |
| 5 | Register all new layers in DI | `MauiProgram.cs` | ⏳ Pending |

---

## Later — Retrieve / Update / Delete (Admin Section)

| Operation | Where | Status |
|-----------|-------|--------|
| Retrieve — list all users | Admin page | ⏳ Future |
| Retrieve — get user by ID | Admin page | ⏳ Future |
| Update — edit user details | Admin page | ⏳ Future |
| Delete — remove user | Admin page | ⏳ Future |

---

## Reference

- FDD: [FDD.md](FDD.md)
- TDD: [TDD.md](TDD.md)
- Schema: [UserAccount_v1.md](../../../database/sqlite/schemas/UserAccount_v1.md)
