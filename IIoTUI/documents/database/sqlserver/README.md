# MariVshApp — SQL Server (Phase 3)

**Status:** Not started — future phase  
**Trigger:** Azure enterprise integration requirement

---

## Planned Contents

| Document | Description |
|----------|-------------|
| SCHEMA.md | SQL Server table definitions (will mirror PostgreSQL schema) |
| CHANGELOG.md | Schema changes specific to SQL Server phase |
| CONNECTION.md | Azure SQL connection configuration (references Key Vault — no credentials here) |

---

## When This Phase Begins

- Copy schema from `../postgresql/SCHEMA.md` as the starting point
- Implement `Repositories/SqlServer/UserAccountRepository.cs`
- Implement `Repositories/SqlServer/UserTypeRepository.cs`
- Add Key Vault secret: `marivshapp-dev-sqlserver-connectionstring`
- Update `MauiProgram.cs` DI registration
- Create ADR 005 — SQL Server selection and Azure SQL strategy
- Validate LSP: SqlServer repositories must fully substitute SQLite repositories

---

## Reference

- Migration plan: `../MIGRATION_PLAN.md`
- Interface contracts: `marivshapp/Repositories/Interfaces/`
