# Dev Embedded — Work Log
## Date: 2026-06-02
## Time: 00:00:00
## Subject: iot_clock_kickoff

### What Was Done
- Reviewed Sprint 1 firmware: clock_product/src/main.cpp
- Confirmed all Sprint 1 features working: displayName(), displayTime(), screen alternation
- Reviewed library dependencies: U8g2, RTClib, Adafruit BusIO
- Created dev_embedded_agent.md with role definition and tech focus
- Firmware module pattern confirmed: functional decomposition in main.cpp

### Sprint 1 — Delivered Firmware
```cpp
displayName()   // "Vishnu" / "Kiran" on OLED
displayTime()   // HH:MM:SS + DD-MM-YYYY on OLED
setup()         // Wire, OLED, RTC init; optional SET_TIME
loop()          // millis()-based rotation + 1s refresh
```

### Decisions Made
- All display functions are self-contained (clear buffer + draw + send)
- SWITCH_INTERVAL = 60000ms constant — clean, readable
- SET_TIME = false by default — safe, no accidental time reset on reflash

### Pending / Next Steps
- Sprint 2: Implement displayTemp() using rtc.getTemperature()
- Sprint 2: Extend rotation cycle (name → time → temp)
- Sprint 2: Implement WiFi connect + NTP sync in setup()
- Wait for Architect ADR on NTP approach before implementing
