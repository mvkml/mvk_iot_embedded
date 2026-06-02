# Scrum Team Master Template

Copy this `_master_template/` folder into any new project and fill in the placeholders.

## Files in `team/`

| File | Role | What to Customise |
|------|------|-------------------|
| `scrum_master_agent.md` | Scrum Master | Nothing — fully generic, reuse as-is |
| `product_owner_agent.md` | Product Owner | `[PROJECT_NAME]`, module names, integration names |
| `architect_agent.md` | Architect | `[PROJECT_NAME]`, tech stack, integration folder name |
| `dev_frontend_agent.md` | Frontend Dev | Framework, UI folder paths, integration name |
| `dev_backend_agent.md` | Backend Dev | Language, framework, ORM, API folder path |
| `dev_sql_agent.md` | Database Dev | Database engine, ORM, domain name |
| `dev_devops_agent.md` | DevOps Engineer | CI/CD platform, board platform |
| `tech_interviewer_agent.md` | Tech Interviewer | Nothing — fully generic, reuse as-is |
| `dev_qa_agent.md` | QA Engineer | QA framework, QA folder path |

## Team Version History
Changes to the team roster are tracked in `versions/`:

| Version | Date | Change |
|---------|------|--------|
| `versions/001_master_template.md` | 2026-05-03 | Initial 7-role team |
| `versions/002_master_template.md` | 2026-05-14 | Tech Interviewer added (8 roles) |
| `versions/003_master_template.md` | 2026-05-16 | Dev QA Agent added (9 roles) |

## Placeholder Reference

| Placeholder | Example (MVK HR) | Your Value |
|-------------|-----------------|------------|
| `[PROJECT_NAME]` | mvkhr | your-project |
| `[DOMAIN]` | HR | Finance / Retail / Logistics |
| `[FRONTEND_FRAMEWORK]` | Angular | React / Vue / Next.js |
| `[BACKEND_TECH]` | FastAPI | Express / Django / Spring |
| `[LANGUAGE]` | Python | Node.js / Java / Go |
| `[DATABASE_ENGINE]` | PostgreSQL | MySQL / MSSQL / MongoDB |
| `[ORM]` | SQLAlchemy | Prisma / Hibernate / Mongoose |
| `[KEY_INTEGRATION]` | Graphify / Azure AI Search | Stripe / Salesforce / OpenAI |
| `[CICD_PLATFORM]` | Azure DevOps | GitHub Actions / Jenkins |
| `[BOARD_PLATFORM]` | Azure DevOps | Jira / Linear / GitHub Projects |
| `[UI_FOLDER]` | mvkhrui | src / apps / frontend |
| `[API_FOLDER]` | mvkapi | backend / api / server |
| `[QA_FRAMEWORK]` | Playwright | Pytest / Cypress / Selenium |
| `[QA_FOLDER]` | mvk_qa | tests / qa / e2e |

## Folder Structure to Create in New Project

```
your-project/
└── agile/
    ├── team/                        ← copy files from _master_template/team/
    ├── product_owner/
    │   ├── backlog/BACKLOG.md
    │   ├── roadmap/ROADMAP.md
    │   ├── user_stories/
    │   └── acceptance_criteria/
    ├── scrum/
    │   ├── sprints/sprint_01/SPRINT_PLAN.md
    │   ├── tasks/
    │   └── user_stories/
    ├── architecture/
    │   ├── decisions/NAMING_CONVENTION.md
    │   ├── tech_stack/TECH_STACK.md
    │   └── diagrams/DIAGRAMS.md
    └── worklogs/
        ├── scrum_master/
        ├── product_owner/
        ├── architect/
        ├── dev_frontend/
        ├── dev_backend/
        ├── dev_sql/
        ├── dev_devops/
        ├── tech_interviewer/
        └── dev_qa/
```

## Worklog Naming Convention (never changes)
```
YYYYMMDD_HHMMSS_subject.md
Example: 20260503_143000_project_kickoff.md
```
