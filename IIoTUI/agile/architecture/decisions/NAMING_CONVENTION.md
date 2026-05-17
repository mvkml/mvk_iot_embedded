# 🏗️ Naming Convention — Owned by Architect Agent

## Worklog File Naming Convention

All worklog files across all agents must follow this format:

```
YYYYMMDD_HHMMSS_subject.md
```

### Breakdown
| Part        | Format       | Example        |
|-------------|--------------|----------------|
| Date        | YYYYMMDD     | 20260503       |
| Time        | HHMMSS       | 143000         |
| Subject     | snake_case   | project_kickoff|
| Extension   | .md          | .md            |

### Full Example
```
20260503_143000_project_kickoff.md
20260503_160000_fastapi_setup.md
20260504_090000_graphify_research.md
```

---

## Worklog Folder Structure

```
agile/worklogs/
├── architect/
│   └── 20260503_000000_project_kickoff.md
├── product_owner/
│   └── 20260503_000000_project_kickoff.md
├── scrum_master/
│   └── 20260503_000000_project_kickoff.md
├── dev_angular/
│   └── 20260503_000000_project_kickoff.md
├── dev_sql/
│   └── 20260503_000000_project_kickoff.md
├── dev_fastapi/
│   └── 20260503_000000_project_kickoff.md
└── dev_devops/
    └── 20260503_000000_project_kickoff.md
```

---

## Worklog File Template

Each worklog file must contain:

```markdown
# [Agent Name] — Work Log
## Date: YYYY-MM-DD
## Time: HH:MM:SS
## Subject: [subject]

### What Was Done
-

### Decisions Made
-

### Pending / Next Steps
-
```

---

## Rules (Enforced by Scrum Master)
- One file per session per agent
- File name must match date/time of session
- Subject must be short and descriptive (max 4 words, snake_case)
- No vague subjects like `misc` or `work`

---
*Defined by: Architect Agent | Date: 2026-05-03*
