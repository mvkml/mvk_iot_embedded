# SQLite DB Artifacts

Versioned snapshots of the `marivshapp.db3` database file for development reference.

## Why this exists

The live DB is stored in the OS `AppDataDirectory` — a path controlled by the OS/platform, not the project:

```
Windows (dev): C:\Users\<user>\AppData\Local\com.companyname.marivshapp\marivshapp.db3
Android:       /data/data/com.companyname.marivshapp/files/marivshapp.db3
iOS:           <AppSandbox>/Library/marivshapp.db3
```

Since the live DB is outside the project, we keep a manual snapshot here after any schema change so we can:
- Inspect table structure and seed data without running the app
- Compare schema across versions
- Catch regressions before they reach other platforms

---

## Naming Convention

```
marivshapp_v<N>.db3
```

| File | Schema Version | Date | Notes |
|------|---------------|------|-------|
| marivshapp_v1.db3 | v1 | 2026-05-17 | Initial: UserAccount + UserType tables |

---

## How to Take a Snapshot

Run `backup_db.ps1` from the project root or PowerShell terminal:

```powershell
.\documents\artifacts\database\sqlite\backup_db.ps1
```

This copies the live DB from AppData into this folder with the correct version name.

---

## How to Inspect

Open any `.db3` file with [DB Browser for SQLite](https://sqlitebrowser.org/) (free).

- **Database Structure** tab — verify table columns and types
- **Browse Data** tab — verify seed data (UserType rows)
- **Execute SQL** tab — run queries manually

---

## Important

- Never commit a DB file that contains real user data
- These snapshots are for schema inspection only — treat them as dev-only artifacts
- After every schema migration (table drop/recreate), take a new snapshot and bump the version
