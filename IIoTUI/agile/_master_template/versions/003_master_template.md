# Scrum Team Master Template — Version 003
## Effective From: 2026-05-16
## Effective Until: —
## Change: Dev QA Agent added — 9 roles

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
| 8 | Tech Interviewer | Knowledge reinforcement via Q&A after each sprint | `agile/worklogs/tech_interviewer/` |
| 9 | Dev QA | Automated testing, validation, bug reporting | `agile/worklogs/dev_qa/` |

**Total: 9 roles**

---

## New Placeholder

| Placeholder | Example (Playwright) | Description |
|-------------|---------------------|-------------|
| `[QA_FRAMEWORK]` | Playwright | QA automation framework |
| `[QA_FOLDER]` | mvk_qa | QA project folder name |

---

## Worklog Convention
```
agile/worklogs/<agent_name>/YYYYMMDD_HHMMSS_subject.md
Example: agile/worklogs/dev_qa/20260516_000100_login_flow_test.md
```

## Agent Interaction Rules
- Architect writes TDD/ADR → notifies Dev agents via worklog
- Architect approves archive decisions → formally intimates DevOps via worklog
- Scrum Master tracks all blockers and resolutions — owns all worklogs
- DevOps owns git check-in, archiving, and environment setup
- Product Owner owns backlog priority — Dev agents do not self-assign stories
- Tech Interviewer runs Q&A sessions after each sprint/implementation to reinforce learning
- **Dev QA validates features against acceptance criteria before sprint sign-off**
- **Dev QA reports bugs to Scrum Master — Dev agents fix before sign-off**

## Previous Version
- `agile/_master_template/versions/002_master_template.md` — 8-role team (before Dev QA)
