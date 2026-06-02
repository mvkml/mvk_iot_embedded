# SH1106 OLED — Component Notes

## Overview
- **Display**: 1.3" OLED, monochrome (white on black)
- **Resolution**: 128 x 64 pixels
- **Controller IC**: SH1106
- **Interface**: I2C (this project) — also available in SPI variant
- **I2C Address**: 0x3C (default) — some boards have 0x3D via solder jumper
- **Operating Voltage**: 3.3V (direct ESP32 compatible)
- **Library**: U8g2 (`olikraus/U8g2@^2.35.19`)

---

## Wiring (I2C)

| OLED Pin | ESP32 Pin | Notes |
|----------|-----------|-------|
| VCC | 3.3V | Do NOT connect to 5V |
| GND | GND | Common ground |
| SDA | GPIO21 | Shared I2C bus |
| SCL | GPIO22 | Shared I2C bus |

---

## Library: U8g2

### Constructor Used
```cpp
#include <U8g2lib.h>

U8G2_SH1106_128X64_NONAME_F_HW_I2C u8g2(U8G2_R0, U8X8_PIN_NONE);
// F = Full buffer mode (128x64 / 8 = 1024 bytes in RAM)
// HW_I2C = Hardware I2C (uses Wire.h)
// U8G2_R0 = No rotation
// U8X8_PIN_NONE = No reset pin
```

### Initialization
```cpp
u8g2.setBusClock(100000);   // match Wire.setClock()
u8g2.begin();
```

### Common Functions
```cpp
u8g2.clearBuffer();                         // clear internal RAM buffer
u8g2.setFont(u8g2_font_helvB14_tf);         // set font
u8g2.drawStr(x, y, "text");                 // draw string (y = baseline)
u8g2.sendBuffer();                          // push buffer to display

u8g2.setFont(u8g2_font_helvB10_tf);         // smaller font
u8g2.drawStr(0, 50, "second line");
```

### Fonts Used in This Project
| Font | Size | Used For |
|------|------|----------|
| `u8g2_font_helvB14_tf` | ~14px | Primary text (time, name) |
| `u8g2_font_helvB10_tf` | ~10px | Secondary text (date) |

### Display Layout (128x64)
```
┌────────────────────────────────┐  y=0
│                                │
│  Line 1 (y=24, helvB14)        │  y=24 (baseline)
│                                │
│  Line 2 (y=50, helvB10/14)     │  y=50 (baseline)
│                                │
└────────────────────────────────┘  y=64
  x=0                         x=128
```

---

## Display Modes (U8g2 Buffer Options)

| Mode | Constructor Suffix | RAM Usage | Notes |
|------|--------------------|-----------|-------|
| Full buffer | `_F_` | 1024 bytes | Smoothest — draw all, send once |
| Page buffer | `_1_` | 128 bytes | Low RAM — draw in pages |
| Page buffer | `_2_` | 256 bytes | Medium RAM |

**This project uses Full buffer (`_F_`)** — ESP32 has enough RAM.

---

## I2C Address Note
- Default: **0x3C**
- Alternate: 0x3D (requires solder bridge on module)
- Shares I2C bus with DS3231 (0x68) — no address conflict

---

## Gotchas
- `y` coordinate in `drawStr()` is the **text baseline**, not the top of the character
- Always call `clearBuffer()` before drawing, then `sendBuffer()` after
- SH1106 is similar to SSD1306 but has 132-column controller mapped to 128px — use SH1106-specific constructor, not SSD1306
- `U8G2_R0` = no rotation; use `U8G2_R2` for 180° flip if mounted upside down
