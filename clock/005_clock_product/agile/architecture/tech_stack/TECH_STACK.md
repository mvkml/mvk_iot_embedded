# Clock Product — Tech Stack

## Firmware
| Layer | Technology | Purpose |
|-------|-----------|---------|
| Language | C++11 (Arduino subset) | Firmware logic |
| Framework | Arduino (ESP32 Arduino Core) | Hardware abstraction |
| Build System | PlatformIO | Dependency management, compile, flash |
| Board | ESP32 (esp32dev) | Microcontroller — 240MHz, 4MB Flash, WiFi+BT |

## Hardware Peripherals
| Component | Library | Purpose |
|-----------|---------|---------|
| SH1106 OLED 128x64 | U8g2 | Display driver (full buffer, I2C) |
| DS3231 RTC | Adafruit RTClib | Real-time clock, temperature sensor |
| I2C Bus | Wire.h (built-in) | SDA=GPIO21, SCL=GPIO22, 100kHz |
| Adafruit BusIO | Adafruit BusIO | RTClib I2C dependency |

## Development Tools
| Tool | Purpose |
|------|---------|
| PlatformIO CLI | Build, upload, serial monitor |
| VS Code + PlatformIO IDE | Code editing and debugging |
| Serial Monitor (115200 baud) | Runtime debug output |
| Git | Version control |

## Future Stack (Sprint 2+)
| Layer | Technology | Purpose |
|-------|-----------|---------|
| WiFi | WiFi.h (built-in ESP32) | Network connectivity |
| NTP | NTPClient or time.h | Time synchronization |
| MQTT | PubSubClient | IoT telemetry (Sprint 4) |
| OTA | ArduinoOTA | Over-the-air firmware update |

## DevOps
| Tool | Purpose |
|------|---------|
| Git | Source control |
| GitHub | Remote repository |
| Agile/Scrum (Light) | Project management |
| platformio.ini | Build configuration |
