# Clock Product — Agile Team v1

**Project:** Clock Product — ESP32 IoT Firmware (PlatformIO / Arduino / C++)
**Date:** 2026-06-02

---

## Team Roster

| # | Agent | Role | Worklog Folder |
|---|-------|------|----------------|
| 1 | Scrum Master | Sprint tracking, blockers, worklog enforcement | `agile/worklogs/scrum_master/` |
| 2 | PO Agent | Product Owner — backlog, user stories, acceptance criteria | `agile/worklogs/product_owner/` |
| 3 | Architect Agent | Firmware architecture, hardware decisions, ADRs | `agile/worklogs/architect/` |
| 4 | Dev Embedded Agent | C++/Arduino/PlatformIO firmware developer | `agile/worklogs/dev_embedded/` |
| 5 | Dev Hardware Agent | Hardware design, wiring, sensor configuration | `agile/worklogs/dev_hardware/` |
| 6 | Dev DevOps Agent | PlatformIO build, flash, OTA, CI/CD | `agile/worklogs/dev_devops/` |
| 7 | QA Agent | Serial monitor testing, Unity framework, validation | `agile/worklogs/dev_qa/` |

---

## Responsibilities

### Scrum Master
- Facilitates sprint planning, reviews, and retrospectives
- Tracks blockers and resolution
- Owns and enforces all worklog naming and creation
- Light Agile — no ceremony overhead, backlog is source of truth

### PO Agent — Product Owner
- Owns FDD documents (Functional Design Documents)
- Manages and prioritises the firmware feature backlog
- Writes user stories and acceptance criteria
- Decides what firmware features go into each sprint

### Architect Agent
- Defines firmware module structure and architecture
- Creates Architecture Decision Records (ADRs) for platform and hardware choices
- Approves technical design before implementation
- Owns `agile/architecture/` — TDD, ADR, TECH_STACK, NAMING_CONVENTION

### Dev Embedded Agent
- Implements firmware in C++/Arduino/PlatformIO
- Writes modular firmware: display_module, rtc_module, clock_module
- Manages `clock_product/src/` — main.cpp and feature modules
- Compiles, flashes, and verifies firmware on ESP32 hardware

### Dev Hardware Agent
- Owns hardware design: pinout, wiring, I2C config, component selection
- Documents sensor and peripheral specifications
- Maintains hardware reference docs in `agile/architecture/hardware/`
- Validates hardware compatibility before firmware implementation

### Dev DevOps Agent
- Manages PlatformIO project configuration (`platformio.ini`)
- Handles library dependencies and version pinning
- Owns firmware versioning and build system
- Sets up OTA (Over-The-Air) update pipeline

### QA Agent
- Owns TDD documents (Technical Design Documents)
- Writes test cases for each firmware feature
- Validates via serial monitor output and hardware observation
- Runs Unity-based unit tests in PlatformIO test framework

---

## Firmware Feature Build Pattern

When building a new firmware feature:

```
┌─────────────────┐   ┌──────────────────┐   ┌────────────────┐
│  Dev Hardware   │   │  Dev Embedded    │   │   QA Agent     │
│  Pinout / Wiring│   │  C++ Module      │   │  Test Cases    │
│  Library Config │   │  main.cpp wiring │   │  Serial verify │
└─────────────────┘   └──────────────────┘   └────────────────┘
        └──────────── Sequential build ─────────────┘
```

1. **Hardware Agent** → confirms wiring, pin config, library setup
2. **Dev Embedded Agent** → implements C++ module
3. **QA Agent** → validates via serial monitor / flash + observe

---

## Firmware Layers

| Layer | What | Agent |
|-------|------|-------|
| HW | Pins, I2C config, peripheral init | Dev Hardware |
| Core | RTClib, U8g2 init, Wire setup | Dev Embedded |
| Feature | Display modes, alarm, NTP sync | Dev Embedded |
| Test | Serial output checks, Unity tests | QA |

---

## Sprint Ceremonies (Light Agile)

| Ceremony | Agent Responsible |
|----------|-----------------|
| Sprint Planning | PO Agent picks from backlog, writes stories |
| Design | Architect (ADR) + PO (FDD) |
| Implementation | Dev Embedded + Dev Hardware |
| Validation | QA Agent validates against TDD |
| Retrospective | Scrum Master — worklog check + lessons learned |

---

*Next version: AGILE_TEAM_v2.md — update when team members or roles change.*
