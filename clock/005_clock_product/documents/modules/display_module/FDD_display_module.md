# Functional Design Document — Display Module

## Document Info

| Field | Value |
|-------|-------|
| Module | display_module |
| Version | v1.0 |
| Date | 2026-06-02 |
| Status | Active |
| Author | Dev Embedded Agent |

---

## 1. Purpose

The Display Module is responsible for rendering all visual content on the SH1106 OLED screen. It owns what is shown, when it is shown, and how it is formatted. No other module writes to the display directly.

---

## 2. Scope

| In Scope | Out of Scope |
|----------|-------------|
| Rendering name screen | Deciding which screen to show (that is loop() in main.cpp) |
| Rendering time + date screen | Reading time from RTC (that is rtc_module) |
| Rendering temperature screen | Reading temperature from RTC (that is rtc_module) |
| Display initialization | I2C bus setup (that is main.cpp setup()) |
| Font and layout decisions | Button input or brightness control (future modules) |

---

## 3. Screens

### Screen 1 — Name Display

| Property | Value |
|----------|-------|
| Trigger | `displayState == 0` |
| Duration | 60 seconds |
| Content | Two names on separate lines |

```
┌────────────────────────────────┐
│                                │
│  Vishnu                        │  ← helvB14, y=24
│                                │
│  Kiran                         │  ← helvB14, y=50
│                                │
└────────────────────────────────┘
```

---

### Screen 2 — Time and Date Display

| Property | Value |
|----------|-------|
| Trigger | `displayState == 1` |
| Duration | 60 seconds |
| Content | Current time (HH:MM:SS) and date (DD-MM-YYYY) |

```
┌────────────────────────────────┐
│                                │
│  11:36:00                      │  ← helvB14, y=24
│                                │
│  02-05-2026                    │  ← helvB10, y=50
│                                │
└────────────────────────────────┘
```

---

### Screen 3 — Temperature Display

| Property | Value |
|----------|-------|
| Trigger | `displayState == 2` |
| Duration | 60 seconds |
| Content | Label "Temp" and current DS3231 sensor reading |

```
┌────────────────────────────────┐
│                                │
│  Temp                          │  ← helvB14, y=24
│                                │
│  28.2 C                        │  ← helvB14, y=50
│                                │
└────────────────────────────────┘
```

---

## 4. Screen Rotation

Screens rotate in a fixed cycle managed by `main.cpp`:

```
Name (60s) → Time (60s) → Temp (60s) → Name (60s) → ...
```

- Rotation interval: **60 seconds** (defined as `SWITCH_INTERVAL` in `config.h`)
- Rotation logic: `displayState = (displayState + 1) % 3`
- Display refreshes every **1 second** via `delay(1000)` in `loop()`

---

## 5. Functional Requirements

| ID | Requirement |
|----|-------------|
| FR01 | Display module shall render the name screen when called |
| FR02 | Display module shall render current time in HH:MM:SS format |
| FR03 | Display module shall render current date in DD-MM-YYYY format |
| FR04 | Display module shall render temperature in XX.X C format |
| FR05 | Each screen shall fully refresh on every call (no partial updates) |
| FR06 | Display module shall initialize the OLED on `initDisplay()` call |
| FR07 | No other module shall write to the OLED directly |

---

## 6. Acceptance Criteria

| Criterion | Pass Condition |
|-----------|---------------|
| Name screen | "Vishnu" and "Kiran" visible on OLED simultaneously |
| Time screen | Time updates every second with correct HH:MM:SS value |
| Date screen | Date shows correct DD-MM-YYYY below the time |
| Temp screen | Temperature value matches DS3231 reading (±1°C tolerance) |
| Screen rotation | Each screen holds for 60s then transitions to next |
| No flicker | Buffer cleared and redrawn cleanly — no partial frame visible |

---

## 7. Dependencies

| Dependency | Why |
|------------|-----|
| `rtc_module` | `displayTime()` and `displayTemp()` read from `rtc` and `getRTCTemperature()` |
| `config.h` | `I2C_CLOCK` for display bus speed |
| U8g2 library | OLED hardware driver |
| SH1106 OLED (hardware) | Physical display device |
