---
name: project-marivshapp
description: "Overview of the MariVshApp .NET MAUI 9 IoT management project — product vision, architecture, key files, and known issues"
metadata: 
  node_type: memory
  type: project
  originSessionId: 9237c530-8105-4b86-a5eb-8be1d5b93014
---

MariVshApp is a .NET MAUI 9 Windows desktop app (IIoTUI workspace, `marivshapp/` folder).
Full context is in `CLAUDE.md` at the project root — read that first in any new session.

## Current State (2026-05-20)

**Done:**
- Login: User ID+Password and 4-digit PIN login
- Sign Up: all fields including PIN, "Go to Login →" link on success
- DB at stable `%LOCALAPPDATA%\MariVshApp\marivshapp.db3` (survives clean/rebuild)
- Repository + Service layers fully wired
- HomePage and AdminPage stubs exist
- Manage page: full CRUD for ManageItem — search toolbar, grid table, Edit/Delete per row
- Item page: Add/Edit form with DatePicker (calendar), Browse image button (copies to %LOCALAPPDATA%\MariVshApp\images\manage_items\), inline validation
- ManageItem SQLite table wired end-to-end (Model → Repository → Service → ViewModel → View)
- Design docs: Login FDD/TDD, SignUp FDD/TDD/Flowchart, HomePage Plan, Manage FDD/TDD/Flowchart
- UI/UX HTML prototypes: `documents/uiux/uiux_marivshapp/v1/` (login, home, manage, item pages)

**Pending:**
- Admin routing (LoginViewModel routes TypeId=2 to AdminPage) — high priority
- Password hashing (ADR 003 PBKDF2) — high priority
- Projects feature (next after Manage) — full CRUD page, navigated from Manage items
- CLAUDE.md update to reflect completed Manage stack

**Why:** Learning/IIoT UI project built incrementally by Vishnu.
