# Sprint 01 — Clock Product

## Goal
Deliver basic clock display firmware: name screen, time/date screen, 60-second alternation.

## Duration
Start: 2026-05-02
End: 2026-06-01
Status: **Completed**

## User Stories in This Sprint
| ID    | Title | Status |
|-------|-------|--------|
| US001 | Basic Clock Display (name + time + date) | ✅ Done |
| US002 | Screen Alternation (60s cycle) | ✅ Done |

## Tasks in This Sprint
| ID | Title | Status |
|----|-------|--------|
| TASK001 | Setup PlatformIO project (ESP32, U8g2, RTClib) | ✅ Done |
| TASK002 | Initialize SH1106 OLED display (I2C, 100kHz) | ✅ Done |
| TASK003 | Initialize DS3231 RTC module (I2C) | ✅ Done |
| TASK004 | Implement displayName() — "Vishnu Kiran" | ✅ Done |
| TASK005 | Implement displayTime() — HH:MM:SS + DD-MM-YYYY | ✅ Done |
| TASK006 | Implement screen alternation (millis() timer, 60s) | ✅ Done |
| TASK007 | Validate via serial monitor + hardware observation | ✅ Done |

## Outcome
- Firmware compiles and flashes to ESP32 via PlatformIO
- OLED displays "Vishnu Kiran" for 60 seconds, then switches to time + date
- DS3231 RTC holds time accurately across power cycles
- SET_TIME compile flag allows one-time time adjustment
