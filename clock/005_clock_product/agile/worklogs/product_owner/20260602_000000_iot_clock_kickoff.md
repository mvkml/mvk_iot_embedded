# Product Owner — Work Log
## Date: 2026-06-02
## Time: 00:00:00
## Subject: iot_clock_kickoff

### What Was Done
- Defined Clock Product firmware backlog (PB001–PB010)
- Created versioned backlog snapshot: 20260602_000000_backlog_clock.md
- Updated product roadmap (Phase 1–4)
- Wrote user stories US001 (clock display) and US002 (screen alternation) — Sprint 1
- Confirmed Sprint 1 features (PB001–PB004) as delivered and working

### Decisions Made
- Sprint 2 priorities: PB005 (temperature) before PB006 (NTP) — simpler, no WiFi needed
- DS3231 temperature sensor included in Sprint 2 (no extra hardware cost)
- WiFi credentials must not be hardcoded in main.cpp — use config header
- OTA (PB010) deferred to Sprint 4 (requires WiFi to be stable first)

### Pending / Next Steps
- Write US003 (temperature display) and US004 (NTP sync) for Sprint 2
- Confirm acceptance criteria with QA Agent before Sprint 2 begins
