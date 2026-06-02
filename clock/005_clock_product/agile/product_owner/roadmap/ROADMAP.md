# Clock Product — Firmware Roadmap

High-level plan for the ESP32 Clock Product firmware.

## Phase 1 — Foundation (Sprint 1) ✅ Complete
- PlatformIO project setup (ESP32, U8g2, RTClib)
- SH1106 OLED display driver initialized
- DS3231 RTC module integrated
- Name display screen ("Vishnu Kiran")
- Time + date display screen (HH:MM:SS, DD-MM-YYYY)
- 60-second screen alternation cycle

## Phase 2 — Sensor + Network (Sprint 2)
- DS3231 temperature sensor reading (onboard, no extra hardware)
- Add temperature screen to rotation cycle
- WiFi connection on startup
- NTP time synchronization (auto-correct RTC drift)
- Sync status indicator on display

## Phase 3 — UX Enhancements (Sprint 3)
- Physical button for manual time zone selection
- OLED brightness control (button-driven)
- Analog clock face mode (graphical, U8g2 draw primitives)
- Display mode cycling (digital / analog / name)

## Phase 4 — IoT Connectivity (Sprint 4)
- Alarm functionality (configurable, trigger LED/buzzer)
- MQTT telemetry (publish time + temp to broker)
- OTA firmware update via WiFi (ArduinoOTA)
- mDNS for device discovery

## Hardware Baseline
- MCU: ESP32-WROOM-32
- Display: SH1106 128x64 OLED (I2C 0x3C)
- RTC: DS3231 (I2C 0x68)
- I2C: SDA=GPIO21, SCL=GPIO22, 100kHz
