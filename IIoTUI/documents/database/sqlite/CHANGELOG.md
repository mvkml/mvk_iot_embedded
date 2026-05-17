# MariVshApp — SQLite Schema Changelog

**Owner:** Dev SQLite Agent  
**Phase:** 1 — SQLite

---

## [2026-05-17] — Phase 1 Initial Schema

**Sprint:** Phase 1 — SQLite foundation  
**Agent:** Dev SQLite

### Tables Created
- `UserType` — TypeId (PK), TypeName
- `UserAccount` — Id (PK), PhoneNumber, FirstName, LastName, Email, DateOfBirth, Gender, UserId, Password, TypeId (FK), CreatedDateTime, UpdatedDateTime

### Seed Data
- UserType: `{1, "User"}`, `{2, "Admin"}`

### Notes
- Tables dropped and recreated on every cold start (dev mode)
- Password stored plain text — flagged for ADR 003 (PBKDF2)

---

<!-- Future entries follow this format:

## [YYYY-MM-DD] — Description

**Sprint:** Sprint name
**Agent:** Dev SQLite

### Added
- Column: `TableName.ColumnName` (TYPE) — reason

### Modified
- Column: `TableName.ColumnName` — old → new — reason

### Removed
- Column: `TableName.ColumnName` — reason

### Notes
- Migration steps or data backfill required

-->
