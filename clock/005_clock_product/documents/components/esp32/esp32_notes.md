# ESP32 — Component Notes

## Overview
- **Module**: ESP32-WROOM-32
- **Chip**: Espressif ESP32 (dual-core Xtensa LX6, 240MHz)
- **Flash**: 4MB
- **RAM**: 520KB SRAM
- **Framework**: Arduino (via PlatformIO)
- **Board ID in platformio.ini**: `esp32dev`

---

## Key Specs

| Property | Value |
|----------|-------|
| CPU | Dual-core Xtensa LX6 @ 240MHz |
| Flash | 4MB |
| SRAM | 520KB |
| WiFi | 802.11 b/g/n 2.4GHz (built-in) |
| Bluetooth | BT 4.2 + BLE (built-in) |
| GPIO | 34 programmable pins |
| ADC | 18 channels (12-bit) |
| DAC | 2 channels (8-bit) |
| I2C | 2 hardware I2C buses |
| SPI | 4 SPI buses (VSPI, HSPI used commonly) |
| UART | 3 UART ports |
| Operating Voltage | 3.3V logic |
| Input Voltage (USB) | 5V via USB or VIN |

---

## GPIO Pin Usage (This Project)

| GPIO | Function | Connected To |
|------|----------|-------------|
| GPIO21 | I2C SDA | SH1106 SDA + DS3231 SDA |
| GPIO22 | I2C SCL | SH1106 SCL + DS3231 SCL |
| GPIO23 | SPI MOSI (VSPI) | SD Card MOSI |
| GPIO19 | SPI MISO (VSPI) | SD Card MISO |
| GPIO18 | SPI SCK (VSPI) | SD Card SCK |
| GPIO5  | SPI CS | SD Card CS |
| GPIO0  | BOOT button | (available for user button Sprint 3) |
| GPIO1  | TX0 (Serial) | USB-Serial debug |
| GPIO3  | RX0 (Serial) | USB-Serial debug |

---

## I2C Configuration (This Project)

```cpp
Wire.begin(21, 22);         // SDA=GPIO21, SCL=GPIO22
Wire.setClock(100000);      // 100kHz — safe for both SH1106 and DS3231
```

---

## SPI Configuration (VSPI — Default)

```cpp
// SD card uses VSPI defaults — no explicit SPI.begin() needed if using default pins
// MOSI=23, MISO=19, SCK=18, CS=5
SD.begin(5);                // CS pin only needed
```

---

## Serial Debug

```cpp
Serial.begin(115200);
```

---

## Power Notes
- 3.3V logic — all peripherals (SH1106, DS3231, SD module) must be 3.3V compatible
- DS3231 has onboard CR2032 backup battery — maintains time during power loss
- SD card module requires a 3.3V regulator if it includes a level shifter onboard

---

## Gotchas
- GPIO34–39 are **input only** — do not use for output
- GPIO0 is BOOT pin — pulling LOW on reset enters flash mode
- ADC2 pins conflict with WiFi — do not use ADC2 when WiFi is active
- Internal pull-ups sufficient at 100kHz I2C — no external resistors needed
- `delay()` blocks the entire CPU — use `millis()` for non-blocking timing
