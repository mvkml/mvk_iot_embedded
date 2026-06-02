# SD Card Module — Component Notes

## Overview
- **Interface**: SPI (Serial Peripheral Interface)
- **SPI Bus**: VSPI (ESP32 default SPI bus)
- **Operating Voltage**: 3.3V logic (most modules include onboard 3.3V regulator + level shifter)
- **Card Format**: FAT32 (recommended, up to 32GB)
- **Library**: `SD.h` — built into ESP32 Arduino core (no `lib_deps` entry needed)

---

## Wiring (SPI — VSPI Default Pins)

| SD Module Pin | ESP32 GPIO | VSPI Function | Notes |
|---------------|-----------|---------------|-------|
| VCC | 3.3V or 5V | — | Check module — most accept 5V and regulate down |
| GND | GND | — | Common ground |
| MOSI | GPIO23 | VSPI MOSI | Master Out Slave In |
| MISO | GPIO19 | VSPI MISO | Master In Slave Out |
| SCK | GPIO18 | VSPI SCK | SPI Clock |
| CS | GPIO5 | VSPI SS | Chip Select (configurable) |

---

## platformio.ini — No Change Needed
```ini
; SD.h is built into ESP32 Arduino core
; No lib_deps entry required
[env:esp32dev]
platform = espressif32
board = esp32dev
framework = arduino
monitor_speed = 115200
lib_deps =
    olikraus/U8g2@^2.35.19
    adafruit/RTClib@^2.1.4
```

---

## Library: SD.h (ESP32 Built-in)

### Initialization
```cpp
#include <SD.h>
#include <SPI.h>    // required — SPI bus init

#define SD_CS_PIN 5

void setup() {
    if (!SD.begin(SD_CS_PIN)) {
        Serial.println("SD card not found!");
        return;
    }
    Serial.println("SD card ready.");
}
```

### Check Card Info
```cpp
uint64_t cardSize = SD.cardSize() / (1024 * 1024);
Serial.printf("Card size: %lluMB\n", cardSize);
Serial.printf("Card type: %d\n", SD.cardType());  // 1=MMC, 2=SD, 3=SDHC
```

### Write to File
```cpp
File f = SD.open("/log.txt", FILE_APPEND);  // FILE_APPEND or FILE_WRITE
if (f) {
    f.println("log entry here");
    f.close();
}
```

### Read from File
```cpp
File f = SD.open("/log.txt");
if (f) {
    while (f.available()) {
        Serial.write(f.read());
    }
    f.close();
}
```

### Check File Exists
```cpp
if (SD.exists("/log.txt")) {
    Serial.println("File exists");
}
```

### Delete File
```cpp
SD.remove("/log.txt");
```

### List Directory
```cpp
File root = SD.open("/");
while (true) {
    File entry = root.openNextFile();
    if (!entry) break;
    Serial.println(entry.name());
    entry.close();
}
```

---

## Use Cases in This Project

| Use Case | Description |
|----------|-------------|
| Temperature log | Append DS3231 temp + timestamp every N minutes |
| Time log | Record NTP sync events with timestamp |
| Config file | Read WiFi credentials or settings from SD card |
| Display messages | Read custom name/message from a file on SD |

---

## SPI Bus Sharing Note
- SD card uses **SPI** (GPIO18/19/23/5)
- SH1106 and DS3231 use **I2C** (GPIO21/22)
- No bus conflict — SPI and I2C are independent buses on ESP32

---

## SD Card Preparation
1. Format as **FAT32** (not exFAT, not NTFS)
2. For cards > 32GB: use a third-party FAT32 formatter (Windows default won't format >32GB as FAT32)
3. Test with a small card first (8GB or 16GB ideal)

---

## Gotchas
- Always call `f.close()` after writing — data may not flush to card without it
- `FILE_WRITE` overwrites from beginning; `FILE_APPEND` adds to end
- SD.begin() returns `false` if card is absent, not formatted FAT32, or CS pin is wrong
- Some SD modules require 5V VCC even though logic is 3.3V — check your specific module
- Long file names not supported — use 8.3 format (e.g., `log.txt`, `data.csv`)
- Do not remove SD card while writing — can corrupt FAT
- `SD_MMC.h` is an alternative library for higher-speed MMC mode (uses different pins)
