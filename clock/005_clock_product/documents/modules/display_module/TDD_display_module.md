# Technical Design Document — Display Module

## Document Info

| Field | Value |
|-------|-------|
| Module | display_module |
| Version | v1.0 |
| Date | 2026-06-02 |
| Status | Active |
| Author | Architect Agent |

---

## 1. Purpose

Describes the internal implementation of the Display Module — file structure, function design, hardware driver configuration, and data flow.

---

## 2. Files

| File | Location | Role |
|------|----------|------|
| `display_module.h` | `src/display_module.h` | Public interface — declarations and extern |
| `display_module.cpp` | `src/display_module.cpp` | Implementation — all display logic |

---

## 3. Public Interface (`display_module.h`)

```cpp
#pragma once

#include <U8g2lib.h>

extern U8G2_SH1106_128X64_NONAME_F_HW_I2C u8g2;

void initDisplay();
void displayName();
void displayTime();
void displayTemp();
```

| Symbol | Type | Description |
|--------|------|-------------|
| `u8g2` | extern object | U8g2 driver instance — exposed for future direct use if needed |
| `initDisplay()` | void | Sets bus clock and calls `u8g2.begin()` |
| `displayName()` | void | Renders Screen 1 (name) |
| `displayTime()` | void | Renders Screen 2 (time + date) |
| `displayTemp()` | void | Renders Screen 3 (temperature) |

---

## 4. Hardware Driver

### U8g2 Constructor

```cpp
U8G2_SH1106_128X64_NONAME_F_HW_I2C u8g2(U8G2_R0, U8X8_PIN_NONE);
```

| Parameter | Value | Meaning |
|-----------|-------|---------|
| `SH1106_128X64_NONAME` | — | SH1106 controller, 128×64 resolution |
| `F` | Full buffer | Full 1024-byte frame buffer in RAM |
| `HW_I2C` | Hardware I2C | Uses ESP32 hardware I2C via Wire.h |
| `U8G2_R0` | No rotation | Display mounted upright |
| `U8X8_PIN_NONE` | No reset pin | Reset pin not wired |

### Initialization

```cpp
void initDisplay() {
    u8g2.setBusClock(I2C_CLOCK);   // must match Wire.setClock()
    u8g2.begin();
}
```

`Wire.begin()` and `Wire.setClock()` are called in `main.cpp` before `initDisplay()` — the display module does not own I2C bus setup.

---

## 5. Function Designs

### 5.1 displayName()

```cpp
void displayName() {
    u8g2.clearBuffer();
    u8g2.setFont(u8g2_font_helvB14_tf);
    u8g2.drawStr(0, 24, "Vishnu");
    u8g2.drawStr(0, 50, "Kiran");
    u8g2.sendBuffer();
}
```

| Step | Call | Purpose |
|------|------|---------|
| 1 | `clearBuffer()` | Wipe internal RAM buffer |
| 2 | `setFont(helvB14)` | 14px bold Helvetica |
| 3 | `drawStr(0, 24, ...)` | Draw "Vishnu" at baseline y=24 |
| 4 | `drawStr(0, 50, ...)` | Draw "Kiran" at baseline y=50 |
| 5 | `sendBuffer()` | Push full buffer to OLED over I2C |

---

### 5.2 displayTime()

```cpp
void displayTime() {
    DateTime now = rtc.now();

    char timeBuf[20];
    char dateBuf[20];
    sprintf(timeBuf, "%02d:%02d:%02d", now.hour(), now.minute(), now.second());
    sprintf(dateBuf, "%02d-%02d-%04d", now.day(), now.month(), now.year());

    u8g2.clearBuffer();
    u8g2.setFont(u8g2_font_helvB14_tf);
    u8g2.drawStr(0, 24, timeBuf);
    u8g2.setFont(u8g2_font_helvB10_tf);
    u8g2.drawStr(0, 50, dateBuf);
    u8g2.sendBuffer();
}
```

| Step | Detail |
|------|--------|
| RTC read | `rtc.now()` — reads `RTC_DS3231` object from `rtc_module` |
| Time format | `%02d:%02d:%02d` → zero-padded HH:MM:SS |
| Date format | `%02d-%02d-%04d` → zero-padded DD-MM-YYYY |
| Time font | `helvB14` — larger, primary line |
| Date font | `helvB10` — smaller, secondary line |

---

### 5.3 displayTemp()

```cpp
void displayTemp() {
    float temp = getRTCTemperature();

    char tempBuf[16];
    sprintf(tempBuf, "%.1f C", temp);

    u8g2.clearBuffer();
    u8g2.setFont(u8g2_font_helvB14_tf);
    u8g2.drawStr(0, 24, "Temp");
    u8g2.drawStr(0, 50, tempBuf);
    u8g2.sendBuffer();
}
```

| Step | Detail |
|------|--------|
| Temp read | `getRTCTemperature()` — calls `rtc.getTemperature()` via rtc_module |
| Format | `%.1f C` → one decimal place, e.g. `28.2 C` |
| Label | Static string "Temp" on line 1 |
| Value | Formatted temperature on line 2 |

---

## 6. Display Layout Reference

```
x=0                                x=128
y=0  ┌────────────────────────────────┐
     │                                │
y=24 │  <line 1 text — helvB14>       │  ← baseline
     │                                │
y=50 │  <line 2 text — helvB10/14>    │  ← baseline
     │                                │
y=64 └────────────────────────────────┘
```

- `drawStr(x, y, ...)` — `y` is the **text baseline**, not the top edge
- Characters extend ~10–14px above the baseline depending on font

---

## 7. Fonts

| Constant | Height | Used On |
|----------|--------|---------|
| `u8g2_font_helvB14_tf` | ~14px | All line 1 text; time on Screen 2 |
| `u8g2_font_helvB10_tf` | ~10px | Date on Screen 2 |

---

## 8. Data Flow

```
main.cpp loop()
    │
    ├─ displayState == 0 ──► displayName()
    │                            └─ u8g2 (local buffer → OLED)
    │
    ├─ displayState == 1 ──► displayTime()
    │                            ├─ rtc.now()  [from rtc_module]
    │                            └─ u8g2
    │
    └─ displayState == 2 ──► displayTemp()
                                 ├─ getRTCTemperature()  [from rtc_module]
                                 └─ u8g2
```

---

## 9. Dependencies

| Dependency | Header | Used In |
|------------|--------|---------|
| U8g2 library | `<U8g2lib.h>` | display_module.cpp — OLED driver |
| rtc_module | `"rtc_module.h"` | displayTime(), displayTemp() |
| config.h | `"config.h"` | `I2C_CLOCK` in initDisplay() |

---

## 10. Future Extensions

| Feature | Change Required |
|---------|----------------|
| SD card log screen | Add `displaySDLog()` — same pattern |
| Analog clock face | Add `displayAnalog()` using `u8g2.drawCircle()` / `drawLine()` |
| Brightness control | Add `u8g2.setContrast(value)` call — no structural change |
| Custom message from SD | Pass `const char*` to a generic `displayMessage()` function |
