# US001 — Basic Clock Display

**Sprint:** Sprint 01
**Status:** ✅ Done
**Priority:** High

## User Story
As a user, I want the desk clock to show my name and the current time/date on the OLED display, so I can use it as a personal desk clock.

## Acceptance Criteria
- [ ] OLED displays "Vishnu" on line 1, "Kiran" on line 2 (name screen)
- [ ] OLED displays current time as HH:MM:SS (time screen)
- [ ] OLED displays current date as DD-MM-YYYY below the time
- [ ] DS3231 RTC provides accurate time (persists across power cycles)
- [ ] SET_TIME=true compile flag allows one-time time adjustment
- [ ] Serial monitor logs "RTC initialized" on startup

## Tasks
| ID | Title |
|----|-------|
| TASK001 | Setup PlatformIO project |
| TASK002 | Initialize SH1106 OLED |
| TASK003 | Initialize DS3231 RTC |
| TASK004 | Implement displayName() |
| TASK005 | Implement displayTime() |

## Hardware Dependencies
- SH1106 128x64 OLED on I2C 0x3C
- DS3231 RTC on I2C 0x68
- I2C: SDA=GPIO21, SCL=GPIO22, 100kHz
