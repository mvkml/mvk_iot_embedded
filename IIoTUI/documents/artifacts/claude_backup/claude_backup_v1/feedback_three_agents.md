---
name: feedback-three-agents
description: Use DB + Dev + UI agents in parallel only when building multiple independent modules at once
metadata: 
  node_type: memory
  type: feedback
  originSessionId: 9237c530-8105-4b86-a5eb-8be1d5b93014
---

User agreed to use three parallel agents (DB Agent, Dev Agent, UI Agent) only when it genuinely helps — not for every feature.

**When to use:**
- Building M08 Hub, M09 Room, M10 Component, M11 Device — all four need the same full stack (Model → Repo → Service → ViewModel → XAML). This is the right time to split across three agents in parallel.

**When NOT to use:**
- Single feature builds — dependencies (Model → Repo → Service → VM → XAML) mean agents block each other anyway.

**How to apply:**
- Flag to user when we reach the IoT modules (Hub/Room/Component/Device) and suggest the three-agent split at that point.
- Do not suggest agents for smaller single-module tasks.
