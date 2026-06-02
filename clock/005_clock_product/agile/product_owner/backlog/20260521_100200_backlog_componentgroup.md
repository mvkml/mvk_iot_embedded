# Backlog — ComponentGroup Module
**File:** `20260521_100200_backlog_componentgroup.md`
**Module:** ComponentGroup (logical grouping of components within a Room)
**Created:** 2026-05-21

---

## Overview

ComponentGroup is a logical grouping of Components within a SubSite/Room.
Example: "Lights Group", "Fans Group", "Entertainment Group" within Hall room.

**Reference entity:** `VIKIApp.Entities.ComponentGroup`

**MariVshApp model name:** `ComponentGroup`

---

## Reference Fields (VIKIApp ComponentGroup)

| Field | Type | Notes |
|-------|------|-------|
| Id | int PK auto | Local row ID |
| ComponentGroupId | int | Business ID |
| Name | string | Group name |
| ImageUrl | string | Associated image |
| Description | string | |
| CreatedDate | string | |
| UpdateDate | string | |
| IsActive | int | |
| Parent | int | Parent group ID (for nested groups) |
| SubSiteId | int | FK → SubSite.SubSiteId |
| DefaultImageId | int | FK → AppImage |
| DisableImageId | int | FK → AppImage |
| BackGroundImageId | int | FK → AppImage |

---

## MariVshApp Planned Fields

| Field | Type | Notes |
|-------|------|-------|
| Id | int PK auto | |
| GroupId | int | Business/external ID |
| RoomId | int | FK → Room.Id |
| ParentGroupId | int? | FK → ComponentGroup.Id (nested groups) |
| Name | string | Group name |
| Description | string | |
| ImageId | int? | FK → AppImage |
| IsActive | bool | |
| CreatedDate | DateTime | |
| UpdatedDate | DateTime | |

---

## Backlog Tasks

### DB Layer

| ID | Task | Priority | Status |
|----|------|----------|--------|
| CG-01 | Create `ComponentGroup` model (`Models/ComponentGroup.cs`) with all planned fields | 🔴 High | ⬜ Pending |
| CG-02 | Create `IComponentGroupRepository` interface (`Repositories/Interfaces/`) | 🔴 High | ⬜ Pending |
| CG-03 | Create `ComponentGroupRepository` SQLite implementation (`Repositories/SQLite/`) | 🔴 High | ⬜ Pending |
| CG-04 | Add `CreateTable<ComponentGroup>()` to `DatabaseService` | 🔴 High | ⬜ Pending |

### Service Layer

| ID | Task | Priority | Status |
|----|------|----------|--------|
| CG-05 | Create `ComponentGroupService` (`Services/ComponentGroup/ComponentGroupService.cs`) | 🔴 High | ⬜ Pending |
| CG-06 | Implement `AddAsync(ComponentGroup)` → validates Name, GroupId unique per Room | 🟡 Medium | ⬜ Pending |
| CG-07 | Implement `UpdateAsync(ComponentGroup)` | 🟡 Medium | ⬜ Pending |
| CG-08 | Implement `DeleteAsync(int id)` | 🟡 Medium | ⬜ Pending |
| CG-09 | Implement `SearchAsync(string text)` → filter by Name/Description | 🟡 Medium | ⬜ Pending |
| CG-10 | Implement `GetByRoomAsync(int roomId)` → filter groups by Room | 🟡 Medium | ⬜ Pending |

### ViewModel

| ID | Task | Priority | Status |
|----|------|----------|--------|
| CG-11 | Create `ManageComponentGroupsViewModel` (`ViewModels/ComponentGroup/`) | 🔴 High | ⬜ Pending |
| CG-12 | Create `ComponentGroupViewModel` (`ViewModels/ComponentGroup/`) — add/edit form | 🔴 High | ⬜ Pending |
| CG-13 | Bind `Rooms` picker (FK dropdown) in `ComponentGroupViewModel` | 🟡 Medium | ⬜ Pending |
| CG-14 | Bind `ParentGroups` picker (optional self-FK for nesting) in `ComponentGroupViewModel` | 🟢 Low | ⬜ Pending |
| CG-15 | Bind `AppImages` picker in `ComponentGroupViewModel` | 🟢 Low | ⬜ Pending |

### View / XAML

| ID | Task | Priority | Status |
|----|------|----------|--------|
| CG-16 | Create `ManageComponentGroupsPage.xaml` — dark theme table: Id, GroupId, Room, Name, IsActive, Actions | 🔴 High | ⬜ Pending |
| CG-17 | Create `ComponentGroupPage.xaml` — add/edit form: GroupId, Name, Description, Room picker, Parent picker, Image picker, IsActive | 🔴 High | ⬜ Pending |
| CG-18 | Wire Edit / Delete buttons in ManageComponentGroupsPage | 🟡 Medium | ⬜ Pending |
| CG-19 | Wire Save / Save & Next / Cancel in ComponentGroupPage | 🟡 Medium | ⬜ Pending |

### DI + Navigation

| ID | Task | Priority | Status |
|----|------|----------|--------|
| CG-20 | Register `IComponentGroupRepository`, `ComponentGroupRepository`, `ComponentGroupService`, `ManageComponentGroupsViewModel`, `ComponentGroupViewModel`, `ManageComponentGroupsPage`, `ComponentGroupPage` in `MauiProgram.cs` | 🔴 High | ⬜ Pending |
| CG-21 | Add Shell routes in `AppShell.xaml.cs` | 🔴 High | ⬜ Pending |
| CG-22 | Add "Component Groups" link in Manage menu sidebar | 🟡 Medium | ⬜ Pending |

### Integration

| ID | Task | Priority | Status |
|----|------|----------|--------|
| CG-23 | Component list filters by selected ComponentGroup | 🟡 Medium | ⬜ Pending |
| CG-24 | ComponentGroup FK dropdown available in Components and ManageComponent modules | 🟡 Medium | ⬜ Pending |

---

## Notes
- `Parent` field supports nested groups (e.g. "All Lights" → "Hall Lights") — implement as optional `ParentGroupId` nullable FK
- `SubSiteId` in VIKIApp → `RoomId` in MariVshApp
- Real example: Hall has "Lights" group (2 tube lights, 1 pendant) + "Fans" group (1 ceiling fan)
