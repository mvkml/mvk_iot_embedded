# Architect — Work Log
## Date: 2026-06-02
## Time: 00:00:00
## Subject: iot_clock_kickoff

### What Was Done
- Created ADR001: ESP32 + PlatformIO as firmware platform (documented rationale)
- Updated TECH_STACK.md for embedded stack (ESP32, Arduino, PlatformIO, U8g2, RTClib)
- Updated architect_agent.md with firmware architectural rules
- Documented I2C bus configuration: SDA=GPIO21, SCL=GPIO22, 100kHz
- Confirmed firmware module pattern: functional decomposition (displayName, displayTime, displayTemp)

### Decisions Made
- ESP32 chosen over Arduino Uno/Nano: onboard WiFi essential for Sprint 2+ features
- PlatformIO chosen over Arduino IDE: dependency management + CLI + CI/CD support
- Full-buffer mode (U8G2_...F...) for SH1106: smoother display, ESP32 has enough RAM
- I2C at 100kHz (safe for both SH1106 and DS3231 on shared bus)
- millis() non-blocking timer for screen alternation (not FreeRTOS tasks — too complex for this stage)

### Pending / Next Steps
- Create ADR002 for NTP sync approach (NTPClient library vs configTime())
- Document WiFi config pattern (config header vs hardcoded)
- Review Sprint 2 firmware modules before Dev Embedded starts implementation
