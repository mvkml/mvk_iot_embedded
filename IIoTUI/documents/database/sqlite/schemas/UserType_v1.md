# UserType — Schema v1

**Table:** `UserType`  
**Version:** v1  
**Date:** 2026-05-17  
**Status:** Active  

---

## Columns

| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| TypeId | INTEGER | PK | Unique type identifier (manually assigned) |
| TypeName | TEXT | NOT NULL | Display name of the user type |
| Description | TEXT | | Description of this type's access level |
| IsActive | INTEGER | | 1 = active, 0 = inactive |
| CreatedDateTime | TEXT | | Row creation timestamp |
| UpdatedDateTime | TEXT | | Last update timestamp |

---

## Default Seed Data

Inserted automatically on first launch by `DatabaseService`:

| TypeId | TypeName | Description | IsActive |
|--------|----------|-------------|----------|
| 1 | User | Default user with basic access rights | 1 |
| 2 | Admin | Administrative user with full access | 1 |

---

## Notes

- `TypeId` is manually assigned (not AutoIncrement) — seed values are stable across restarts
- `ToString()` returns `TypeName` — used by MAUI Picker binding in SignUpPage
- `GetAllActiveAsync()` returns only rows where `IsActive = 1` — used by sign up picker
- New types can be added in Admin section in future (no code change needed)

---

## Repository

`Repositories/Interfaces/IUserTypeRepository.cs` — interface  
`Repositories/SQLite/UserTypeRepository.cs` — implementation

## Model File

`Models/UserType.cs`
