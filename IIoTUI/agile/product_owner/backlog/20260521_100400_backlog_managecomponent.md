# Backlog — ManageComponent (Device Assignment) Module
**File:** `20260521_100400_backlog_managecomponent.md`
**Module:** ManageComponent (MariVshApp equivalent: Device — fan, light, TV, fish tank)
**Created:** 2026-05-21

---

## Overview

ManageComponent is the configured/assigned device instance — the "running configuration" that links
a Component/Node to a specific Project → Hub → Room → Group hierarchy and defines its control settings.

**Reference entity:** `VIKIApp.Entities.ManageComponent`

**MariVshApp model name:** `Device` (or `ManageComponent`)

This is the most complex entity — it denormalizes the full hierarchy for fast querying and holds
control parameters (switch state, schedule, sensor thresholds, favourites).

---

## Reference Fields (VIKIApp ManageComponent — key fields)

| Field | Type | Notes |
|-------|------|-------|
| Id | int PK auto | |
| ManageId | int | Business ID |
| Name | string | Device/assignment name |
| Description | string | |
| VikiId | string | Parent Hub hardware ID |
| ChildVikiId | string | Component node hardware ID |
| IsDefault | int | Default configuration flag |
| ProjectId | int | FK → Project |
| SiteId | int | FK → Site/Hub |
| SubSiteId | int | FK → SubSite/Room |
| ComponentGroupId | int | FK → ComponentGroup |
| ComponentId | int | FK → Component |
| IsFavourite | int | Favourites flag |
| SwitchOn | int | Current switch state (0/1) |
| StartTime | string | Scheduled ON time |
| EndTime | string | Scheduled OFF time |
| JsonObject | string | ManageComponentJson (extended config) |
| DeviceId | int | Physical device type ID |
| CategoryId | int | FK → Category |
| ComponentUrl | string | HTTP endpoint URL for ESP32 control |
| ComponentsSetId | string | Group set identifier |
| NormalityValue | string | Normal threshold value |
| TypeId | int | Component type |
| IsActive | int | |
| CreatedDate | string | |
| UpdatedDate | string | |

---

## MariVshApp Planned Fields

| Field | Type | Notes |
|-------|------|-------|
| Id | int PK auto | |
| DeviceId | int | Business/external ID |
| Name | string | Device display name |
| Description | string | |
| ProjectId | int | FK → Project.Id |
| HubId | int | FK → Hub.Id |
| RoomId | int | FK → Room.Id |
| GroupId | int | FK → ComponentGroup.Id |
| ComponentId | int | FK → Component.Id |
| CategoryId | int? | FK → Category.Id |
| HubNodeId | string | ESP32 Hub hardware ID |
| ComponentNodeId | string | ESP32 Component node ID |
| ComponentUrl | string | HTTP endpoint for control (e.g. http://192.168.1.x/relay) |
| IsFavourite | bool | Shown on Favourites home page |
| IsDefault | bool | Default config |
| SwitchOn | bool | Current ON/OFF state |
| StartTime | string? | Scheduled ON time |
| EndTime | string? | Scheduled OFF time |
| TypeId | int? | Device type |
| IsActive | bool | |
| CreatedDate | DateTime | |
| UpdatedDate | DateTime | |

---

## Backlog Tasks

### DB Layer

| ID | Task | Priority | Status |
|----|------|----------|--------|
| MC-01 | Create `Device` model (`Models/Device.cs`) with all planned fields | 🔴 High | ⬜ Pending |
| MC-02 | Create `IDeviceRepository` interface (`Repositories/Interfaces/`) | 🔴 High | ⬜ Pending |
| MC-03 | Create `DeviceRepository` SQLite implementation (`Repositories/SQLite/`) | 🔴 High | ⬜ Pending |
| MC-04 | Add `CreateTable<Device>()` to `DatabaseService` | 🔴 High | ⬜ Pending |

### Service Layer

| ID | Task | Priority | Status |
|----|------|----------|--------|
| MC-05 | Create `DeviceService` (`Services/Device/DeviceService.cs`) | 🔴 High | ⬜ Pending |
| MC-06 | Implement `AddAsync(Device)` → validates Name, ComponentId unique per Room | 🟡 Medium | ⬜ Pending |
| MC-07 | Implement `UpdateAsync(Device)` | 🟡 Medium | ⬜ Pending |
| MC-08 | Implement `DeleteAsync(int id)` | 🟡 Medium | ⬜ Pending |
| MC-09 | Implement `SearchAsync(string text)` → filter by Name | 🟡 Medium | ⬜ Pending |
| MC-10 | Implement `GetByRoomAsync(int roomId)` → all devices in a Room | 🟡 Medium | ⬜ Pending |
| MC-11 | Implement `GetFavouritesAsync()` → all IsFavourite=true devices | 🔴 High | ⬜ Pending |
| MC-12 | Implement `ToggleSwitchAsync(int id)` → flip SwitchOn + send HTTP to ComponentUrl | 🔴 High | ⬜ Pending |
| MC-13 | Implement `GetByProjectAsync(int projectId)` | 🟢 Low | ⬜ Pending |

### ViewModel

| ID | Task | Priority | Status |
|----|------|----------|--------|
| MC-14 | Create `ManageDevicesViewModel` (`ViewModels/Device/`) — list + search + add/edit/delete | 🔴 High | ⬜ Pending |
| MC-15 | Create `DeviceViewModel` (`ViewModels/Device/`) — full add/edit form with FK pickers | 🔴 High | ⬜ Pending |
| MC-16 | Bind `Projects` picker in `DeviceViewModel` | 🟡 Medium | ⬜ Pending |
| MC-17 | Bind `Hubs` picker (filtered by Project) in `DeviceViewModel` | 🟡 Medium | ⬜ Pending |
| MC-18 | Bind `Rooms` picker (filtered by Hub) in `DeviceViewModel` | 🟡 Medium | ⬜ Pending |
| MC-19 | Bind `Groups` picker (filtered by Room) in `DeviceViewModel` | 🟡 Medium | ⬜ Pending |
| MC-20 | Bind `Components` picker (filtered by Group) in `DeviceViewModel` | 🟡 Medium | ⬜ Pending |
| MC-21 | Bind `Categories` picker in `DeviceViewModel` | 🟢 Low | ⬜ Pending |

### View / XAML

| ID | Task | Priority | Status |
|----|------|----------|--------|
| MC-22 | Create `ManageDevicesPage.xaml` — dark theme table: Id, DeviceId, Name, Hub, Room, Group, Component, SwitchOn, IsFavourite, IsActive, Actions | 🔴 High | ⬜ Pending |
| MC-23 | Create `DevicePage.xaml` — full add/edit form with cascading FK pickers + control fields | 🔴 High | ⬜ Pending |
| MC-24 | Wire Edit / Delete buttons in ManageDevicesPage | 🟡 Medium | ⬜ Pending |
| MC-25 | Wire Save / Cancel in DevicePage | 🟡 Medium | ⬜ Pending |
| MC-26 | On/Off toggle button in ManageDevicesPage row — calls `ToggleSwitchAsync` | 🔴 High | ⬜ Pending |

### DI + Navigation

| ID | Task | Priority | Status |
|----|------|----------|--------|
| MC-27 | Register `IDeviceRepository`, `DeviceRepository`, `DeviceService`, `ManageDevicesViewModel`, `DeviceViewModel`, `ManageDevicesPage`, `DevicePage` in `MauiProgram.cs` | 🔴 High | ⬜ Pending |
| MC-28 | Add Shell routes in `AppShell.xaml.cs` | 🔴 High | ⬜ Pending |
| MC-29 | Add "Devices" link in Manage menu sidebar | 🟡 Medium | ⬜ Pending |

### Integration

| ID | Task | Priority | Status |
|----|------|----------|--------|
| MC-30 | Favourites home page loads `GetFavouritesAsync()` and shows toggle buttons | 🔴 High | ⬜ Pending |
| MC-31 | `ToggleSwitchAsync` sends HTTP GET to `ComponentUrl` (e.g. `http://<esp32-ip>/relay?state=1`) | 🔴 High | ⬜ Pending |
| MC-32 | Cascade FK selection: Project → Hub → Room → Group → Component (each picker filters the next) | 🟡 Medium | ⬜ Pending |

---

## Notes
- This is the most complex module — depends on Hub, Room, ComponentGroup, Component, Project, Category all being complete first
- `ComponentUrl` = ESP32 HTTP endpoint — this is the actual IoT control path: `http://192.168.x.x/relay?state=1`
- `IsFavourite` drives the Home page Favourites section
- `SwitchOn` is the local cached state; real state confirmed via ESP32 HTTP response
- `JsonObject` in VIKIApp stores extended config — evaluate if needed in MariVshApp v1 (likely defer)
- Schedule fields (`StartTime`, `EndTime`) — defer to v2 unless automation is v1 requirement
