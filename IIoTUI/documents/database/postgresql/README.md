# MariVshApp — PostgreSQL (Phase 2)

**Status:** Not started — future phase  
**Trigger:** When shared/multi-device data or cloud backend is needed

---

## Planned Contents

| Document | Description |
|----------|-------------|
| SCHEMA.md | PostgreSQL table definitions (will mirror SQLite schema) |
| CHANGELOG.md | Schema changes specific to PostgreSQL phase |
| CONNECTION.md | Connection configuration (references Key Vault secrets — no credentials here) |

---

## When This Phase Begins

- Copy schema from `../sqlite/SCHEMA.md` as the starting point
- Implement `Repositories/PostgreSQL/UserAccountRepository.cs`
- Implement `Repositories/PostgreSQL/UserTypeRepository.cs`
- Add Key Vault secret: `marivshapp-dev-postgresql-connectionstring`
- Update `MauiProgram.cs` DI registration
- Create ADR 004 — PostgreSQL selection and connection strategy

---

## Reference

- Migration plan: `../MIGRATION_PLAN.md`
- Interface contracts: `marivshapp/Repositories/Interfaces/`
