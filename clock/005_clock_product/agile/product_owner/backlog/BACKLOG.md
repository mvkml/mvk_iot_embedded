# Clock Product — Firmware Backlog

List of all firmware features, enhancements, and fixes.
Prioritized by the Product Owner.

| ID    | Feature | Description | Priority | Status |
|-------|---------|-------------|----------|--------|
| PB001 | Name Display | Show "Vishnu Kiran" on OLED screen 1 | High | ✅ Done |
| PB002 | Time Display | Show HH:MM:SS on OLED screen 2 | High | ✅ Done |
| PB003 | Date Display | Show DD-MM-YYYY below time | High | ✅ Done |
| PB004 | Screen Alternation | Rotate name/time every 60 seconds | High | ✅ Done |
| PB005 | Temperature Display | Read DS3231 onboard temp sensor, add screen 3 | Medium | To Do |
| PB006 | NTP Time Sync | Connect WiFi on startup, sync time from NTP | Medium | To Do |
| PB007 | Alarm Functionality | Configurable alarm with buzzer/LED trigger | Low | To Do |
| PB008 | Analog Clock Face | Graphical analog clock on OLED | Low | To Do |
| PB009 | OLED Brightness | Physical button to cycle brightness levels | Low | To Do |
| PB010 | OTA Update | Over-the-air firmware update via WiFi | Low | To Do |

## Notes
- Sprint 1 (completed): PB001–PB004 — basic clock display
- Sprint 2 (planned): PB005 (temperature) + PB006 (NTP sync)
- DS3231 has built-in temperature sensor (±3°C) — free feature, no extra hardware
- WiFi required for PB006, PB010 — ESP32 has onboard WiFi
