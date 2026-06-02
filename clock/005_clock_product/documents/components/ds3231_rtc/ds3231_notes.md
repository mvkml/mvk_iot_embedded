# DS3231 RTC — Component Notes

## Overview
- **Component**: DS3231 — Real-Time Clock with temperature-compensated crystal
- **Interface**: I2C
- **I2C Address**: 0x68 (fixed — cannot change)
- **Operating Voltage**: 3.3V (direct ESP32 compatible)
- **Accuracy**: ±2 ppm (very high — drifts < 1 minute per year)
- **Backup Battery**: CR2032 coin cell — maintains time during power loss
- **Bonus**: Onboard temperature sensor (±3°C accuracy)
- **Library**: Adafruit RTClib (`adafruit/RTClib@^2.1.4`)

---

## Wiring (I2C)

| DS3231 Pin | ESP32 Pin | Notes |
|------------|-----------|-------|
| VCC | 3.3V | |
| GND | GND | Common ground |
| SDA | GPIO21 | Shared I2C bus |
| SCL | GPIO22 | Shared I2C bus |
| SQW | — | Square wave output (not used) |
| 32K | — | 32kHz output (not used) |

---

## Library: Adafruit RTClib

### Setup
```cpp
#include <RTClib.h>

RTC_DS3231 rtc;

void setup() {
    if (!rtc.begin()) {
        Serial.println("RTC not found!");
        while (true);
    }
}
```

### Set Time (Compile-time, one-shot)
```cpp
#define SET_TIME  true   // flip to false after first upload

if (SET_TIME) {
    rtc.adjust(DateTime(2026, 6, 2, 10, 30, 0));  // Y, M, D, H, Min, Sec
}
```

### Read Time
```cpp
DateTime now = rtc.now();

int h = now.hour();       // 0–23
int m = now.minute();     // 0–59
int s = now.second();     // 0–59
int d = now.day();        // 1–31
int mo = now.month();     // 1–12
int y = now.year();       // e.g. 2026

char buf[20];
sprintf(buf, "%02d:%02d:%02d", h, m, s);
sprintf(dateBuf, "%02d-%02d-%04d", d, mo, y);
```

### Read Temperature
```cpp
float temp = rtc.getTemperature();   // returns float, e.g. 28.25
// Accuracy: ±3°C — good enough for room temperature display

char tempBuf[16];
sprintf(tempBuf, "%.1f C", temp);    // e.g. "28.2 C"
```

### NTP Write-back (Sprint 2)
```cpp
// After NTP sync, write time to DS3231:
rtc.adjust(DateTime(ntpYear, ntpMonth, ntpDay, ntpHour, ntpMinute, ntpSecond));
```

---

## I2C Address Note
- Fixed at **0x68** — cannot be changed
- Shares I2C bus with SH1106 (0x3C) — no address conflict

---

## Battery Backup
- CR2032 coin cell on module
- Maintains date/time when main power is off
- Replace battery if time resets unexpectedly after power loss

---

## Temperature Sensor Notes
- Built into DS3231 chip — no extra hardware needed
- Updates every 64 seconds internally
- Accuracy: ±3°C — suitable for ambient room display
- Call `rtc.getTemperature()` — returns `float`
- Used in Sprint 2 (F05 — Temperature Display)

---

## Gotchas
- Always check `rtc.begin()` returns `true` — halt if not found
- `SET_TIME` flag must be set back to `false` after time is set, or time resets every reboot
- `DateTime` months are 1-based (January = 1), unlike some C libraries
- `rtc.lostPower()` returns `true` if battery died and time is invalid — handle gracefully
- DS3231 uses 24-hour format — no AM/PM conversion needed
