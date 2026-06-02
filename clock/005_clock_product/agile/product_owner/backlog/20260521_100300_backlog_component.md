# Backlog — Component (Node) Module
**File:** `20260521_100300_backlog_component.md`
**Module:** Component (MariVshApp equivalent: Component/Node — ESP32 + Relay)
**Created:** 2026-05-21

---

## Overview

Component is the hardware node — maps to **Component/Node (ESP32 + Relay)** in MariVshApp.
This is the actual ESP32 relay board that controls the physical device (fan, light, TV, fish tank).
A Component belongs to a ComponentGroup and handles On/Off switching for one physical device.

**Reference entity:** `VIKIApp.Entities.Components`

**MariVshApp model name:** `Component` (or `Node`)

---

## Reference Fields (VIKIApp Components)

| Field | Type | Notes |
|-------|------|-------|
| Id | int PK auto | Local row ID |
| ComponentsId | int | Business ID |
| VikiId | string | Hardware/device ID |
| Name | string | Component name |
| ImageUrl | string | Associated image |
| Description | string | |
| CreatedDate | string | |
| UpdateDate | string | |
| IsActive | int | |
| CategoryId | int | FK → Category |
| DefaultImageId | int | FK → AppImage |
| DisableImageId | int | FK → AppImage |
| BackGroundImageId | int | FK → AppImage |
| TypeId | int | Component type |

---

## MariVshApp Planned Fields

| Field | Type | Notes |
|-------|------|-------|
| Id | int PK auto | |
| ComponentId | int | Business/external ID |
| NodeId | string | ESP32 hardware node ID (MAC or assigned) |
| GroupId | int | FK → ComponentGroup.Id |
| CategoryId | int? | FK → Category.Id |
| Name | string | Component name |
| Description | string | |
| DefaultImageId | int? | FK → AppImage (ON state image) |
| DisableImageId | int? | FK → AppImage (OFF state image) |
| TypeId | int? | Component type (relay, dimmer, sensor) |
| IsActive | bool | |
| CreatedDate | DateTime | |
| UpdatedDate | DateTime | |

---

## Backlog Tasks

### DB Layer

| ID | Task | Priority | Status |
|----|------|----------|--------|
| COMP-01 | Create `Component` model (`Models/Component.cs`) with all planned fields | 🔴 High | ⬜ Pending |
| COMP-02 | Create `IComponentRepository` interface (`Repositories/Interfaces/`) | 🔴 High | ⬜ Pending |
| COMP-03 | Create `ComponentRepository` SQLite implementation (`Repositories/SQLite/`) | 🔴 High | ⬜ Pending |
| COMP-04 | Add `CreateTable<Component>()` to `DatabaseService` | 🔴 High | ⬜ Pending |

### Service Layer

| ID | Task | Priority | Status |
|----|------|----------|--------|
| COMP-05 | Create `ComponentService` (`Services/Component/ComponentService.cs`) | 🔴 High | ⬜ Pending |
| COMP-06 | Implement `AddAsync(Component)` → validates Name, NodeId unique | 🟡 Medium | ⬜ Pending |
| COMP-07 | Implement `UpdateAsync(Component)` | 🟡 Medium | ⬜ Pending |
| COMP-08 | Implement `DeleteAsync(int id)` | 🟡 Medium | ⬜ Pending |
| COMP-09 | Implement `SearchAsync(string text)` → filter by Name/NodeId | 🟡 Medium | ⬜ Pending |
| COMP-10 | Implement `GetByGroupAsync(int groupId)` → filter by ComponentGroup | 🟡 Medium | ⬜ Pending |
| COMP-11 | Implement `GetByCategoryAsync(int categoryId)` → filter by Category | 🟢 Low | ⬜ Pending |

### ViewModel

| ID | Task | Priority | Status |
|----|------|----------|--------|
| COMP-12 | Create `ManageComponentsViewModel` (`ViewModels/Component/`) — list + search + add/edit/delete | 🔴 High | ⬜ Pending |
| COMP-13 | Create `ComponentViewModel` (`ViewModels/Component/`) — add/edit form with validation | 🔴 High | ⬜ Pending |
| COMP-14 | Bind `ComponentGroups` picker (FK dropdown) in `ComponentViewModel` | 🟡 Medium | ⬜ Pending |
| COMP-15 | Bind `Categories` picker in `ComponentViewModel` | 🟡 Medium | ⬜ Pending |
| COMP-16 | Bind `DefaultImage` and `DisableImage` pickers in `ComponentViewModel` | 🟢 Low | ⬜ Pending |

### View / XAML

| ID | Task | Priority | Status |
|----|------|----------|--------|
| COMP-17 | Create `ManageComponentsPage.xaml` — dark theme table: Id, ComponentId, NodeId, Group, Category, Name, IsActive, Actions | 🔴 High | ⬜ Pending |
| COMP-18 | Create `ComponentPage.xaml` — add/edit form: ComponentId, NodeId, Name, Description, Group picker, Category picker, Image pickers, TypeId, IsActive | 🔴 High | ⬜ Pending |
| COMP-19 | Wire Edit / Delete buttons in ManageComponentsPage | 🟡 Medium | ⬜ Pending |
| COMP-20 | Wire Save / Save & Next / Cancel in ComponentPage | 🟡 Medium | ⬜ Pending |

### DI + Navigation

| ID | Task | Priority | Status |
|----|------|----------|--------|
| COMP-21 | Register `IComponentRepository`, `ComponentRepository`, `ComponentService`, `ManageComponentsViewModel`, `ComponentViewModel`, `ManageComponentsPage`, `ComponentPage` in `MauiProgram.cs` | 🔴 High | ⬜ Pending |
| COMP-22 | Add Shell routes in `AppShell.xaml.cs` | 🔴 High | ⬜ Pending |
| COMP-23 | Add "Components" link in Manage menu sidebar | 🟡 Medium | ⬜ Pending |

### Integration

| ID | Task | Priority | Status |
|----|------|----------|--------|
| COMP-24 | Component FK dropdown available in ManageComponent module | 🟡 Medium | ⬜ Pending |
| COMP-25 | On/Off state image display driven by DefaultImageId / DisableImageId | 🟢 Low | ⬜ Pending |

---

## Notes
- `VikiId` in VIKIApp → `NodeId` in MariVshApp (ESP32 MAC address or assigned node ID)
- `TypeId` — could be: 1=Relay, 2=Dimmer, 3=Sensor — define enum in v1
- `DefaultImageId` = image shown when component is ON; `DisableImageId` = image when OFF
- Real example: "Fan Node" (NodeId: ESP32_MAC_001) in Hall Fans Group controls ceiling fan
