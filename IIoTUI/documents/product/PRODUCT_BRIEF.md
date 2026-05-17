# MariVshApp — Product Brief

**Owner:** Product Owner Agent  
**Last Updated:** 2026-05-17  
**Status:** Draft — core product definition captured

---

## What Is MariVshApp?

MariVshApp is an **on-premise IoT management and control application** built with .NET MAUI 9.  
It allows users to configure, manage, and control IoT-enabled physical environments — homes, apartments, industries — from a single mobile/desktop interface.

The hardware ecosystem (Hub + Components) is custom-built by Vishnu using ESP32-based embedded systems.

---

## The Three Layers

```
┌─────────────────────────────────┐
│         MariVshApp              │  ← .NET MAUI 9 mobile/desktop app
│   (Configure + Control UI)      │     This application
└────────────┬────────────────────┘
             │  HTTP request (via Hub URL)
             ▼
┌─────────────────────────────────┐
│             HUB                 │  ← Custom embedded device (ESP32)
│   (Central controller per       │     Built by Vishnu
│    project — manages all nodes) │     Contains SD Card + RTC Clock
└────────────┬────────────────────┘
             │  Routes to Component URL
             ▼
┌─────────────────────────────────┐
│          COMPONENT (Node)       │  ← Custom embedded device (ESP32 + Relay)
│   (One per physical device —    │     Built by Vishnu
│    fan, light, TV, fish tank)   │     Attached directly to the device
└─────────────────────────────────┘
             │
             ▼
     Physical Device (Fan / Light / TV / Fish Tank / ...)
```

---

## Core Concept — The "Project"

A **Project** is one physical environment that has a full IoT setup.

| Environment | Project Example |
|-------------|----------------|
| House | "My 2BHK Home" — full home automation |
| Apartment | Single unit in a building |
| Industry | Factory, warehouse, office |

Each project is independent. It has:
- One **Hub** (the central controller)
- Many **Components** (one per physical device being automated)

---

## Hardware — Hub

| Property | Detail |
|----------|--------|
| Built by | Vishnu (custom embedded) |
| Processor | ESP32 |
| Storage | SD Card |
| Clock | RTC (Real-Time Clock) |
| Role | Central controller for one project |
| Manages | All components (nodes) within the project |
| Configured via | Hub URL — set in MariVshApp |

The Hub is the brain of a project. All component commands pass through the Hub.

---

## Hardware — Component (Node)

| Property | Detail |
|----------|--------|
| Built by | Vishnu (custom embedded) |
| Processor | ESP32 |
| Actuator | Relay (controls the physical device) |
| Role | Controls one physical device (fan, light, TV, etc.) |
| Configured via | Component URL — set in MariVshApp |

One Component per physical device that needs automation.  
The Component is physically attached to (or wired into) the device.

---

## Real Example — 2BHK House Project

**Project:** "My Home" — 2 Bedrooms, 1 Hall, 1 Kitchen

### Hall

| Device | Component | Automated? |
|--------|-----------|------------|
| Fan | Component (ESP32 + Relay) | ✅ |
| Light 1 | Component (ESP32 + Relay) | ✅ |
| Light 2 | Component (ESP32 + Relay) | ✅ |
| Tube Light | Component (ESP32 + Relay) | ✅ |
| Television | Component (ESP32 + Relay) | ✅ |
| Fish Tank | Component (ESP32 + Relay) | ✅ |

> Bedroom, Kitchen details to be added.

---

## How Control Works (The Flow)

```
User taps "Turn ON Fan" in MariVshApp
        ↓
App sends HTTP request to Hub URL
        ↓
Hub receives request — identifies the component (fan)
        ↓
Hub sends command to Component URL (fan's ESP32)
        ↓
Component (ESP32 + Relay) activates
        ↓
Fan turns ON
```

---

## What MariVshApp Configures

| Configuration | Description |
|---------------|-------------|
| Hub URL | The network address of the Hub for this project |
| Component URL | The network address of each component (per device) |
| Component → Device mapping | Which component controls which physical device |
| Component → Room mapping | Which room the device is in |

> Configuration details (screens, fields, validation) to be defined in upcoming sprints.

---

## Data Model (Conceptual)

```
Project
  └── Hub (1 per project)
        └── Room (Hall, Bedroom 1, Bedroom 2, Kitchen, ...)
              └── Component/Node (1 per physical device)
                    └── Device (Fan, Light, TV, Fish Tank, ...)
```

> Exact schema (tables, columns) to be defined when Repository Pattern sprint begins for these entities.

---

## Users and Roles

| Role | Expected Access |
|------|----------------|
| User (TypeId = 1) | View and control components within their project |
| Admin (TypeId = 2) | Full access — manage projects, hubs, components, users |

> Role permissions to be defined in upcoming sprints.

---

## Deployment Model

- **On-premise** — app and hub communicate on the local network
- No cloud dependency for core control (hub is local)
- Platform: Windows, Android, iOS, macOS (.NET MAUI 9)
- Database: SQLite Phase 1 (local device storage)

---

## Open Questions

- [ ] How does the app discover the Hub URL — manual entry or auto-discovery?
- [ ] Is Hub communication HTTP, MQTT, or WebSocket?
- [ ] Can one project have multiple Hubs?
- [ ] How are multiple projects managed — does one user own multiple projects?
- [ ] What happens if the Hub is offline — does the app show status?
- [ ] Bedroom and Kitchen component list — to be provided
- [ ] What does the User home screen look like after login?

---

## Related Documents

| Document | Link |
|----------|------|
| Database Schema | `documents/database/sqlite/SCHEMA.md` |
| Architecture Decisions | `agile/architecture/decisions/` |
| Team | `agile/team/` |
