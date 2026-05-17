# UserAccount — Schema v1

**Table:** `UserAccount`  
**Version:** v1  
**Date:** 2026-05-17  
**Status:** Active  

---

## Columns

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

---

## Notes

- `UserId` uniqueness enforced at DB level via `[Unique]` attribute
- `Password` is plain text — flagged as security blocker — ADR 003 pending
- `UserTypeId = 1` → routes to HomePage on login
- `UserTypeId = 2` → routes to AdminPage (not yet wired in LoginViewModel)
- `IsActive` defaults to `true` on signup

---

## Model File

`Models/UserAccount.cs`
