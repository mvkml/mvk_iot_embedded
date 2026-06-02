# Agentic Team Plan v1 — IoT Modules Build

**Created:** 2026-05-21
**Modules:** M09 Site | M10 Subsite | M11 Component Group | M12 Component
**Strategy:** 3 agents run in parallel, each owns one layer across all 4 modules

---

## The Team

| Agent | Role | Owns |
|-------|------|------|
| **Agent 1 — DB Agent** | Data layer | Models, Repositories, DatabaseService table + seed |
| **Agent 2 — Dev Agent** | Logic layer | Services, ViewModels |
| **Agent 3 — UI Agent** | View layer | XAML Pages, Code-behind |
| **Claude Main** | Wiring + Integration | MauiProgram.cs, AppShell.xaml.cs, final build |
| **Vishnu** | Owner | Review, test, sign-off each module |

---

## Work Assignment Table

### Agent 1 — DB Agent

| Module | File | Task | Status |
|--------|------|------|--------|
| M09 Site | `Models/Site.cs` | Model with all fields + SQLite attributes | ❌ |
| M09 Site | `Repositories/Interfaces/ISiteRepository.cs` | Interface (GetAll, GetById, Search, NameExists, Insert, Update, Delete) | ❌ |
| M09 Site | `Repositories/SQLite/SiteRepository.cs` | Full implementation | ❌ |
| M09 Site | `Database/DatabaseService.cs` | Add `CreateTableAsync<Site>()` + ManageItem seed | ❌ |
| M10 Subsite | `Models/Subsite.cs` | Model with all fields + SQLite attributes | ❌ |
| M10 Subsite | `Repositories/Interfaces/ISubsiteRepository.cs` | Interface | ❌ |
| M10 Subsite | `Repositories/SQLite/SubsiteRepository.cs` | Full implementation | ❌ |
| M10 Subsite | `Database/DatabaseService.cs` | Add `CreateTableAsync<Subsite>()` + ManageItem seed | ❌ |
| M11 ComponentGroup | `Models/ComponentGroup.cs` | Model with all fields + SQLite attributes | ❌ |
| M11 ComponentGroup | `Repositories/Interfaces/IComponentGroupRepository.cs` | Interface | ❌ |
| M11 ComponentGroup | `Repositories/SQLite/ComponentGroupRepository.cs` | Full implementation | ❌ |
| M11 ComponentGroup | `Database/DatabaseService.cs` | Add `CreateTableAsync<ComponentGroup>()` + ManageItem seed | ❌ |
| M12 Component | `Models/Component.cs` | Model with all fields + SQLite attributes | ❌ |
| M12 Component | `Repositories/Interfaces/IComponentRepository.cs` | Interface | ❌ |
| M12 Component | `Repositories/SQLite/ComponentRepository.cs` | Full implementation | ❌ |
| M12 Component | `Database/DatabaseService.cs` | Add `CreateTableAsync<Component>()` + ManageItem seed | ❌ |

---

### Agent 2 — Dev Agent

| Module | File | Task | Status |
|--------|------|------|--------|
| M09 Site | `Services/Site/SiteService.cs` | SearchAsync, AddAsync, UpdateAsync, DeleteAsync | ❌ |
| M09 Site | `ViewModels/Site/ManageSitesViewModel.cs` | List VM — FilteredItems, Search, Add, Edit, Delete, Back, Logout | ❌ |
| M09 Site | `ViewModels/Site/ManageSiteViewModel.cs` | Form VM — IQueryAttributable, Save, SaveNext, Cancel | ❌ |
| M10 Subsite | `Services/Subsite/SubsiteService.cs` | SearchAsync, AddAsync, UpdateAsync, DeleteAsync | ❌ |
| M10 Subsite | `ViewModels/Subsite/ManageSubsitesViewModel.cs` | List VM | ❌ |
| M10 Subsite | `ViewModels/Subsite/ManageSubsiteViewModel.cs` | Form VM — SiteId picker items | ❌ |
| M11 ComponentGroup | `Services/ComponentGroup/ComponentGroupService.cs` | SearchAsync, AddAsync, UpdateAsync, DeleteAsync | ❌ |
| M11 ComponentGroup | `ViewModels/ComponentGroup/ManageComponentGroupsViewModel.cs` | List VM | ❌ |
| M11 ComponentGroup | `ViewModels/ComponentGroup/ManageComponentGroupViewModel.cs` | Form VM — SubsiteId picker items | ❌ |
| M12 Component | `Services/Component/ComponentService.cs` | SearchAsync, AddAsync, UpdateAsync, DeleteAsync | ❌ |
| M12 Component | `ViewModels/Component/ManageComponentsViewModel.cs` | List VM | ❌ |
| M12 Component | `ViewModels/Component/ManageComponentViewModel.cs` | Form VM — ComponentGroupId + ComponentTypeId picker items | ❌ |

---

### Agent 3 — UI Agent

| Module | File | Task | Status |
|--------|------|------|--------|
| M09 Site | `Views/Site/SiteListPage.xaml` | Checklist view — Header, Toolbar (Search + Add), Table, Edit/Delete | ❌ |
| M09 Site | `Views/Site/SiteListPage.xaml.cs` | Code-behind — ManageSitesViewModel constructor | ❌ |
| M09 Site | `Views/Site/ManageSitePage.xaml` | Form — Name, Address, Description, IsActive, dates (edit mode) | ❌ |
| M09 Site | `Views/Site/ManageSitePage.xaml.cs` | Code-behind — ManageSiteViewModel constructor | ❌ |
| M10 Subsite | `Views/Subsite/SubsiteListPage.xaml` | Checklist view — Header, Toolbar, Table, Edit/Delete | ❌ |
| M10 Subsite | `Views/Subsite/SubsiteListPage.xaml.cs` | Code-behind | ❌ |
| M10 Subsite | `Views/Subsite/ManageSubsitePage.xaml` | Form — Name, Site picker, Description, IsActive | ❌ |
| M10 Subsite | `Views/Subsite/ManageSubsitePage.xaml.cs` | Code-behind | ❌ |
| M11 ComponentGroup | `Views/ComponentGroup/ComponentGroupListPage.xaml` | Checklist view — Header, Toolbar, Table, Edit/Delete | ❌ |
| M11 ComponentGroup | `Views/ComponentGroup/ComponentGroupListPage.xaml.cs` | Code-behind | ❌ |
| M11 ComponentGroup | `Views/ComponentGroup/ManageComponentGroupPage.xaml` | Form — Name, Subsite picker, Description, IsActive | ❌ |
| M11 ComponentGroup | `Views/ComponentGroup/ManageComponentGroupPage.xaml.cs` | Code-behind | ❌ |
| M12 Component | `Views/Component/ComponentListPage.xaml` | Checklist view — Header, Toolbar, Table (with TYPE + GROUP cols), Edit/Delete | ❌ |
| M12 Component | `Views/Component/ComponentListPage.xaml.cs` | Code-behind | ❌ |
| M12 Component | `Views/Component/ManageComponentPage.xaml` | Form — Name, Group picker, Type picker, GpioPin, MacAddress, Description, IsActive | ❌ |
| M12 Component | `Views/Component/ManageComponentPage.xaml.cs` | Code-behind | ❌ |

---

### Claude Main — Wiring (after all 3 agents done)

| File | Task | Status |
|------|------|--------|
| `MauiProgram.cs` | Register all 4 Repositories (scoped) | ❌ |
| `MauiProgram.cs` | Register all 4 Services (scoped) | ❌ |
| `MauiProgram.cs` | Register all 8 ViewModels (transient) | ❌ |
| `MauiProgram.cs` | Register all 8 Pages (transient) | ❌ |
| `AppShell.xaml.cs` | Register all 8 routes | ❌ |
| Build | `dotnet build` — 0 errors | ❌ |

---

### Vishnu — Review & Sign-off

| Module | Task | Status |
|--------|------|--------|
| M09 Site | Confirm data model fields before build | ❌ |
| M09 Site | Test: Add / Edit / Delete / Search on SiteListPage | ❌ |
| M10 Subsite | Confirm data model fields before build | ❌ |
| M10 Subsite | Test: Add / Edit / Delete / Search on SubsiteListPage | ❌ |
| M11 ComponentGroup | Confirm data model fields before build | ❌ |
| M11 ComponentGroup | Test: Add / Edit / Delete / Search on ComponentGroupListPage | ❌ |
| M12 Component | Confirm data model fields (PLAN_v1.md) | ❌ |
| M12 Component | Test: Add / Edit / Delete / Search, check pickers | ❌ |
| All | Update MARIVSHAPP_BACKLOG_v3.md to mark M09–M12 Done | ❌ |

---

## Parallel Execution Flow

```
START
  │
  ├── Agent 1 (DB)   ──────────────────────────────────────────────┐
  │   Models + Repos + DB tables (all 4 modules)                    │
  │                                                                  │
  ├── Agent 2 (Dev)  ──────────────────────────────────────────────┤ MERGE
  │   Services + ViewModels (all 4 modules)                         │
  │                                                                  │
  ├── Agent 3 (UI)   ──────────────────────────────────────────────┘
  │   XAML Pages (all 4 modules)
  │
  ▼
Claude Main — Wiring (MauiProgram.cs + AppShell.xaml.cs)
  │
  ▼
Build — 0 errors
  │
  ▼
Vishnu — Test & Sign-off (module by module)
  │
  ▼
DONE — Mark M09–M12 ✅ in BACKLOG_v3
```

---

## Notes

- Agents 1, 2, 3 run **in parallel** — each owns its layer independently across all 4 modules
- Agent 2 (Dev) may reference Agent 1 output (model field names) — based on this plan doc, not waiting for Agent 1 to finish
- Agent 3 (UI) binds to known ViewModel property names defined in this plan — can run fully in parallel
- **CS0118 risk** on Component and ComponentGroup folders: all service/repo files must use `Models.Component` and `Models.ComponentGroup` (fully qualified), no `using MariVshApp.Models;`
- Build order for DB tables (in DatabaseService): Site → Subsite → ComponentGroup → Component (FK dependency order)
