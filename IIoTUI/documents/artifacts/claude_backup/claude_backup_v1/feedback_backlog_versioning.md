---
name: feedback-backlog-versioning
description: "Always create versioned backlog files (v1, v2, ...) not a single overwritten file"
metadata: 
  node_type: memory
  type: feedback
  originSessionId: 9237c530-8105-4b86-a5eb-8be1d5b93014
---

Backlog files must follow the same convention as worklogs: `YYYYMMDD_HHMMSS_backlog_<module>.md`

Examples:
- `20260521_000000_backlog_full.md` — full project backlog
- `20260521_143000_backlog_category.md` — module-specific backlog

**Why:** Date-time comes first so files sort chronologically. Module/context at the end identifies scope.

**How to apply:** Every new backlog or major update = new file with current date-time + backlog + module name. Never overwrite existing backlog files.
