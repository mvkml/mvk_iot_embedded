# Dev Hardware — Work Log
## Date: 2026-06-02
## Time: 00:00:00
## Subject: iot_clock_kickoff

### What Was Done
- Documented hardware pinout for Clock Product v1
- Confirmed I2C bus: SDA=GPIO21, SCL=GPIO22 (ESP32 hardware I2C)
- Confirmed I2C addresses: SH1106=0x3C, DS3231=0x68 (no conflict)
- Created dev_hardware_agent.md with component specs and BOM
- Validated hardware stack is running correctly (Sprint 1 delivered)

### Hardware Configuration — Confirmed Working
```
ESP32 GPIO21 (SDA) → SH1106 SDA + DS3231 SDA (shared)
ESP32 GPIO22 (SCL) → SH1106 SCL + DS3231 SCL (shared)
ESP32 3.3V         → SH1106 VCC + DS3231 VCC
ESP32 GND          → SH1106 GND + DS3231 GND
I2C Clock          → 100kHz (Wire.setClock(100000))
```

### Decisions Made
- Shared I2C bus at 100kHz: confirmed stable for SH1106 + DS3231 simultaneously
- No external pull-up resistors needed (ESP32 internal pull-ups sufficient at 100kHz)
- DS3231 onboard 3.3V coin cell (CR2032) maintains time during power loss
- DS3231 temperature sensor is usable (±3°C accuracy) — no extra sensor needed for Sprint 2

### Pending / Next Steps
- For Sprint 2 (NTP): no new hardware required — ESP32 WiFi is onboard
- For Sprint 3 (button): identify suitable GPIO for push button (suggest GPIO0 — BOOT button)
- Document buzzer wiring if Sprint 3 alarm feature is approved
