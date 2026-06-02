# Sprint 02 — Clock Product

## Goal
Add temperature display from DS3231 onboard sensor and NTP time synchronization via WiFi.

## Duration
Start: 2026-06-02
End: TBD
Status: **Planned**

## User Stories in This Sprint
| ID    | Title | Status |
|-------|-------|--------|
| US003 | Temperature Display (DS3231 sensor) | To Do |
| US004 | NTP Time Sync via WiFi | To Do |

## Tasks in This Sprint

### US003 — Temperature Display
| ID | Title | Status |
|----|-------|--------|
| TASK008 | Read DS3231 temperature via `rtc.getTemperature()` | To Do |
| TASK009 | Implement displayTemp() — show °C on OLED | To Do |
| TASK010 | Add temp screen to rotation cycle (name → time → temp) | To Do |
| TASK011 | Validate temperature reading accuracy via serial monitor | To Do |

### US004 — NTP Time Sync
| ID | Title | Status |
|----|-------|--------|
| TASK012 | Add WiFi credentials (SSID + password) | To Do |
| TASK013 | Implement WiFi connect on setup() | To Do |
| TASK014 | Implement NTP sync — write synced time back to DS3231 | To Do |
| TASK015 | Add WiFi + sync status indicator on OLED startup screen | To Do |
| TASK016 | Handle WiFi unavailable gracefully (use RTC time only) | To Do |

## Notes
- DS3231 has built-in temperature sensor (±3°C accuracy) — no extra hardware needed
- WiFi credentials should NOT be hardcoded in main.cpp — use `#define` in a separate config header
- NTP sync writes to DS3231 so time persists across reboots/WiFi loss
- Rotation becomes: name (60s) → time (60s) → temp (60s) → repeat
