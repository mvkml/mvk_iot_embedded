# Scrum Team Master Template — Version 001
## Effective From: 2026-05-03
## Effective Until: 2026-05-13
## Change: Initial team formation — 7 roles

---

## Team Roster

| # | Agent | Role Summary | Worklog Folder |
|---|-------|-------------|----------------|
| 1 | Scrum Master | Sprint tracking, blockers, worklog enforcement | `agile/worklogs/scrum_master/` |
| 2 | Product Owner | Requirements, FDD, backlog, user stories | `agile/worklogs/product_owner/` |
| 3 | Architect | TDD, ADRs, tech stack, naming conventions | `agile/worklogs/architect/` |
| 4 | Dev Frontend | Frontend implementation ([FRONTEND_FRAMEWORK]) | `agile/worklogs/dev_frontend/` |
| 5 | Dev Backend | Backend implementation ([BACKEND_TECH]) | `agile/worklogs/dev_backend/` |
| 6 | Dev SQL | Database design, queries, migrations | `agile/worklogs/dev_sql/` |
| 7 | Dev DevOps | Git, archiving, environments, CI/CD | `agile/worklogs/dev_devops/` |

**Total: 7 roles**

---

## Worklog Convention
```
agile/worklogs/<agent_name>/YYYYMMDD_HHMMSS_subject.md
Example: agile/worklogs/scrum_master/20260503_000000_project_kickoff.md
```

## Agent Interaction Rules
- Architect writes TDD/ADR → notifies Dev agents via worklog
- Architect approves archive decisions → formally intimates DevOps via worklog
- Scrum Master tracks all blockers and resolutions — owns all worklogs
- DevOps owns git check-in, archiving, and environment setup
- Product Owner owns backlog priority — Dev agents do not self-assign stories

## Folder Structure
```
agile/
  team/                    ← role definition files (copied from _master_template)
  product_owner/
    backlog/
    roadmap/
    user_stories/
    acceptance_criteria/
  architecture/
    decisions/
    tech_stack/
    diagrams/
  scrum/
    sprints/sprint_01/
    tasks/
    user_stories/
    retrospectives/
  worklogs/
    scrum_master/
    product_owner/
    architect/
    dev_frontend/
    dev_backend/
    dev_sql/
    dev_devops/
```

## What Changed in v002
- Tech Interviewer agent added
- See: `agile/_master_template/versions/002_master_template.md`
