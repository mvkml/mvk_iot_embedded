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

## [2026-05-20] — ManageItem Table

**Sprint:** Manage Page — Phase 1

### Added
- Table: `ManageItem` — generic navigation/menu item for the Manage page
  - `Id` (INTEGER, PK AutoIncrement) — SQLite row key
  - `ItemId` (INTEGER) — user-provided identifier
  - `Name` (TEXT) — display name
  - `Description` (TEXT) — optional
  - `ImageUrl` (TEXT) — optional
  - `IsActive` (INTEGER) — active flag
  - `CreatedDate` (TEXT) — auto-set on insert
  - `UpdatedDate` (TEXT) — auto-set on insert/update

### New Files
- `Models/ManageItem.cs`
- `Repositories/Interfaces/IManageItemRepository.cs`
- `Repositories/SQLite/ManageItemRepository.cs`
- `Services/Manage/ManageItemService.cs`

### Notes
- No FK relations in v1 — standalone table
- `ItemId` is generic — will link to specific entities in future phases

---

## [2026-05-20] — ManageItem.NavigateUrl Column

**Sprint:** Manage Page — Navigate URL

### Added
- Column: `ManageItem.NavigateUrl` (TEXT, DEFAULT '') — Shell route name; used by `Go →` button in ManagePage to navigate to the linked module page (e.g. `ProjectListPage`)

### Migration
- `ALTER TABLE ManageItem ADD COLUMN NavigateUrl TEXT NOT NULL DEFAULT ''` runs on startup via `DatabaseService` — safe for existing DBs (try/catch ignored if column already exists)

### Notes
- Empty `NavigateUrl` silently no-ops the `Go →` button
- Value must match a registered Shell route name exactly

---

## [2026-05-21] — ComponentType Table

**Sprint:** Component Module — Phase 1

### Added
- `ComponentType` table — seeded lookup table for component hardware types
  - Columns: TypeId (PK), Name, Description, IsActive, CreatedDate, UpdatedDate
  - Seed data: Switch (1), Monitor (2), InfraRed (3), Alarm (4)
  - Pattern: same as UserType — app-seeded on first run, read-only in UI

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
