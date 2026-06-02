# Claude Code — Folder & Configuration Reference

**Project:** MariVshApp (IIoTUI)  
**Developer:** Vishnu (mvkwithmath@gmail.com)  
**Last updated:** 2026-06-02

---

## Claude Home Folder

```
C:\Users\mvidh\.claude\
```

This is the root of all Claude Code configuration, memory, sessions, and project history.

---

## Claude Home Folder Structure

| Folder / File | Purpose |
|---------------|---------|
| `projects\` | Per-project conversation history and memory files |
| `skills\` | Custom agent skills (slash commands like `/po-agent`) — **not yet created** |
| `sessions\` | Active session data |
| `session-env\` | Session environment snapshots |
| `cache\` | Response cache |
| `backups\` | Claude-managed internal backups |
| `file-history\` | File edit history |
| `ide\` | IDE extension data (VS Code / JetBrains) |
| `plans\` | Plan mode artifacts |
| `plugins\` | Plugin data |
| `shell-snapshots\` | Shell state snapshots |
| `settings.json` | Global Claude Code settings |
| `.credentials.json` | API credentials (do NOT commit or share) |
| `CLAUDE_LOCAL_GUIDE.md` | Local usage guide |

---

## This Project's Claude Folder

**Project name in Claude:**
```
c--git-100-iot-git-mvk-iot-embedded-IIoTUI
```

**Full path:**
```
C:\Users\mvidh\.claude\projects\c--git-100-iot-git-mvk-iot-embedded-IIoTUI\
```

This folder stores:
- Conversation `.jsonl` files (one per session)
- Project-level `memory\` files
- `MEMORY.md` — memory index auto-loaded at session start

---

## Memory Files (Auto-loaded)

Claude Code loads `MEMORY.md` at the start of every session.
Memory files live at:

```
C:\Users\mvidh\.claude\projects\c--git-100-iot-git-mvk-iot-embedded-IIoTUI\memory\
```

Memory types used in this project:

| Type | Purpose |
|------|---------|
| `project_*.md` | Project context, goals, current state |
| `feedback_*.md` | How Claude should behave (learned corrections) |
| `reference_*.md` | External system pointers (VIKIApp, tools) |
| `reminder_*.md` | One-off reminders for specific modules |

---

## Skills Folder (Planned — Not Yet Created)

For agent slash commands (`/po-agent`, `/dev-agent`, etc.) to work, skills must be installed at:

```
C:\Users\mvidh\.claude\skills\
```

Each skill lives in its own subfolder with a `SKILL.md` file:

```
C:\Users\mvidh\.claude\skills\
├── po-agent\SKILL.md
├── dev-agent\SKILL.md
├── db-agent\SKILL.md
├── ui-agent\SKILL.md
├── qa-agent\SKILL.md
├── iot-agent\SKILL.md
└── claude-agent\SKILL.md
```

Source skill files are in the project repo at:
```
C:\git\100_iot\git\mvk_iot_embedded\IIoTUI\agile\team\
├── po-agent\SKILL.md
├── dev-agent\SKILL.md
├── db-agent\SKILL.md
├── ui-agent\SKILL.md
├── qa-agent\SKILL.md
├── iot-agent\SKILL.md
└── claude-agent\SKILL.md
```

---

## Backup / Restore

### Backup location (this project)

```
C:\git\100_iot\git\mvk_iot_embedded\IIoTUI\documents\artifacts\claude_backup\
├── calude_info.md              ← this file
├── restore_claude.ps1          ← restore script
└── claude_backup_v1\           ← versioned snapshot
    ├── MEMORY.md
    ├── project_marivshapp.md
    ├── feedback_full_path.md
    ├── feedback_backlog_versioning.md
    ├── feedback_three_agents.md
    ├── reference_vikiapp.md
    ├── reminder_componentgroup_selectimage.md
    └── project_work_status_angular.md
```

### To restore Claude memory from a backup

```powershell
cd C:\git\100_iot\git\mvk_iot_embedded\IIoTUI\documents\artifacts\claude_backup
.\restore_claude.ps1 -Version 1
```

This copies `claude_backup_v1\` contents to:
```
C:\Users\mvidh\.claude\projects\c--git-100-iot-git-mvk-iot-embedded-IIoTUI\memory\
```

> After restore, restart Claude Code. If the project folder name differs, rename the encoded folder in `C:\Users\mvidh\.claude\projects\` to match.

---

## Other Claude Projects on This Machine

| Project Folder | Description |
|----------------|-------------|
| `c--git-100-iot-git-mvk-iot-embedded-IIoTUI` | **This project** — MariVshApp (IIoTUI) |
| `c--git-100-iot-git-mvk-iot-embedded` | Parent IoT repo root |
| `c--v-v-learn-lv-python-git-mvk-ai-ml-azure-git-mvk-ai-ml-azure-work-maf` | Azure ML / MAF project |
| `c--v-v-learn-azure-azure-ai-marivshapp` | Azure AI variation of MariVshApp |
| `c--v-v-learn-lv-python-git-mvk-ai-ml-hr-git-mvk-ai-ml-hr` | HR AI project (mvkhr era) |
| `c--v-v-learn-lv-python-git-mvk-net-design-pattern-*` | .NET Design Patterns study |
| `c--v-v-learn-lv-python-git-mvk-mcp-work-mcp` | MCP (Model Context Protocol) work |
| `c--v-v-learn-lv-python-git-mvk-azure-terraform-*` | Terraform / Azure infra |

---

## Quick Reference

| What | Path |
|------|------|
| Claude home | `C:\Users\mvidh\.claude\` |
| This project memory | `C:\Users\mvidh\.claude\projects\c--git-100-iot-git-mvk-iot-embedded-IIoTUI\memory\` |
| Global settings | `C:\Users\mvidh\.claude\settings.json` |
| Skills (install here) | `C:\Users\mvidh\.claude\skills\` |
| Project backup | `documents\artifacts\claude_backup\claude_backup_v1\` |
| Restore script | `documents\artifacts\claude_backup\restore_claude.ps1` |
