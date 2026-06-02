# MariVshApp — Agile Team v1

**Project:** MariVshApp — IoT Management Desktop App (.NET MAUI 9)  
**Date:** 2026-06-02  

---

## Team Roster

| # | Agent | Role | Skill Path |
|---|-------|------|------------|
| 1 | PO Agent | Product Owner | `.claude/skills/po-agent/` |
| 2 | Dev Agent | .NET MAUI Developer | `.claude/skills/dev-agent/` |
| 3 | DB Agent | Database / Repository | `.claude/skills/db-agent/` |
| 4 | UI Agent | XAML / UI Design | `.claude/skills/ui-agent/` |
| 5 | QA Agent | Quality / Testing | `.claude/skills/qa-agent/` |
| 6 | IoT Agent | Hardware / ESP32 | `.claude/skills/iot-agent/` |
| 7 | Claude Agent | Backup / Restore Claude Context | `.claude/skills/claude-agent/` |

---

## Responsibilities

### PO Agent — Product Owner
- Owns FDD documents (Functional Design Documents)
- Manages and prioritises the backlog
- Writes user stories and acceptance criteria
- Updates `CLAUDE.md` Pending / Open Items table
- Updates `work_status.html` task log

### Dev Agent — .NET MAUI Developer
- Implements ViewModels, Services, navigation
- Writes C# code following MVVM + Repository pattern
- Wires DI in `MauiProgram.cs`
- Registers Shell routes in `AppShell.xaml.cs`
- Runs and verifies the app after each change

### DB Agent — Database / Repository
- Owns Models, Repository interfaces and implementations
- Manages `DatabaseService.cs` table creation and seeding
- Writes and reviews schema changes
- Updates `documents/database/sqlite/SCHEMA.md` and `CHANGELOG.md`
- Takes DB backups using `backup_db.ps1`

### UI Agent — XAML / UI Design
- Owns all `.xaml` View files
- Designs page layouts: Header, Sidebar, Table, Form, Footer
- Ensures consistent styling across all pages
- Reviews XAML bindings and DataTriggers

### QA Agent — Quality / Testing
- Owns TDD documents (Technical Design Documents)
- Writes test cases for each page (positive + negative + edge)
- Validates business rules against FDD
- Reviews navigation flows and error messages

### IoT Agent — Hardware / ESP32
- Owns IoT Hub FDD and hardware design docs
- Documents Hub → Component → Device architecture
- Manages `documents/modules/iot/` folder
- Coordinates app ↔ hardware API contracts

### Claude Agent
- Takes versioned backup of Claude memory to `documents/artifacts/claude_backup/`
- Restores context for new developers joining the team
- Maintains `MEMORY.md` index

---

## Three-Agent Parallel Strategy

When building a new IoT module (Hub / Room / Component / Device):

```
┌─────────────┐   ┌─────────────┐   ┌─────────────┐
│  DB Agent   │   │  Dev Agent  │   │  UI Agent   │
│  Model      │   │  ViewModel  │   │  XAML View  │
│  Repository │   │  Service    │   │  Layout     │
│  Schema     │   │  Navigation │   │  Bindings   │
└─────────────┘   └─────────────┘   └─────────────┘
        └──────────── Run in Parallel ─────────────┘
```

Use parallel agents only for IoT module builds. For single-page work, use the relevant single agent.

---

## Sprint Ceremonies (Claude Agent Support)

| Ceremony | Agent Responsible |
|----------|------------------|
| Sprint Planning | PO Agent — picks from backlog, writes stories |
| Design | PO Agent (FDD) + QA Agent (TDD) |
| Development | Dev Agent + DB Agent + UI Agent |
| Review | QA Agent — validates against TDD |
| Retrospective | Context Manager — backup context before end of sprint |

---

*Next version: AGILE_TEAM_v2.md — update when team members or roles change.*
