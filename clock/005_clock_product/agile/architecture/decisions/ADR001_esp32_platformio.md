# ADR001 — ESP32 + PlatformIO as Firmware Platform

**Date:** 2026-06-02
**Status:** Accepted
**Decided by:** Architect Agent

---

## Context
Selecting the microcontroller platform and build system for the Clock Product.
Requirements: I2C display, I2C RTC, future WiFi for NTP + MQTT, C++ development.

## Decision
Use **ESP32** (Espressif ESP32-WROOM-32) with the **Arduino framework** via **PlatformIO**.

## Rationale

| Factor | Decision | Reason |
|--------|----------|--------|
| MCU | ESP32 | Built-in WiFi + BT; sufficient GPIO; Arduino ecosystem |
| Framework | Arduino | Mature library ecosystem; U8g2 + RTClib both support it |
| Build system | PlatformIO | Better than Arduino IDE: dependency management, CI/CD, multi-target |
| Language | C++ | Arduino framework default; sufficient for embedded clock |
| I2C | Hardware I2C (Wire.h) | Reliable, faster than software I2C; ESP32 has dedicated I2C peripherals |

## Alternatives Rejected

| Alternative | Reason Rejected |
|-------------|----------------|
| Arduino Uno/Nano | No WiFi onboard; insufficient RAM for future NTP/MQTT features |
| ESP8266 | Fewer GPIO; less RAM; ESP32 is the natural successor |
| ESP-IDF (native) | Higher complexity; Arduino framework is sufficient for this product |
| Arduino IDE | No dependency management; poor CLI support; PlatformIO is superior |
| Raspberry Pi Pico | No built-in WiFi on basic version; less library support for U8g2/RTClib |

## Consequences
- `platformio.ini` is the single source of build truth
- All library dependencies declared in `platformio.ini` (no manual installs)
- Future WiFi features (NTP, MQTT, OTA) available without hardware change
- ESP32 Arduino core version must be pinned to avoid breaking changes

---

*Architecture Decision Record — Clock Product | Architect Agent | 2026-06-02*
