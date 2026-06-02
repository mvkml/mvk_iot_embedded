# Deployment Guide — ESP32 Clock Product

**Project:** 005_clock_product  
**Target:** ESP32-WROOM-32 (esp32dev)  
**Framework:** Arduino via PlatformIO  
**Last deployed:** 2026-06-02

---

## 1. Prerequisites

### 1.1 Hardware Required
| Item | Detail |
|---|---|
| ESP32-WROOM-32 | Dev board with USB |
| SH1106 OLED | 128×64, I2C, address 0x3C |
| DS3231 RTC | I2C, address 0x57 |
| USB cable | Data cable (not charge-only) |
| PC | Windows / Mac / Linux |

### 1.2 Hardware Wiring
| Signal | ESP32 GPIO | Peripheral |
|---|---|---|
| I2C SDA | GPIO 21 | OLED SDA, RTC SDA |
| I2C SCL | GPIO 22 | OLED SCL, RTC SCL |
| 3.3V | 3V3 pin | OLED VCC, RTC VCC |
| GND | GND | OLED GND, RTC GND |

### 1.3 Software Required
| Tool | Version | Install |
|---|---|---|
| VS Code | Latest | https://code.visualstudio.com |
| PlatformIO extension | Latest | VS Code Extensions marketplace |
| USB driver (CP210x) | Latest | Required for CH340/CP210x USB chips |

---

## 2. Project Structure

```
005_clock_product/
└── clock_product/              ← PlatformIO project root
    ├── platformio.ini          ← Build configuration
    └── src/
        ├── config.h            ← All pin and WiFi defines
        ├── main.cpp            ← setup() and loop()
        ├── display_module.h/cpp
        ├── rtc_module.h/cpp
        ├── wifiService.h/cpp
        └── apiService.h/cpp
```

---

## 3. Configuration Before Flash

Open [clock_product/src/config.h](../../clock_product/src/config.h) and update:

```cpp
// WiFi credentials — must match your router
#define WIFI_SSID        "your_ssid"
#define WIFI_PASSWORD    "your_password"

// RTC time set — set to true once, then back to false
#define SET_TIME         false
#define T_YEAR           2026
#define T_MONTH          6
#define T_DAY            2
#define T_HOUR           14
#define T_MINUTE         0
#define T_SECOND         0
```

> **Important:** If RTC time is wrong, set `SET_TIME true`, flash once, then set back to `false` and flash again. Leaving it `true` resets the clock on every reboot.

---

## 4. Build Steps

### Step 1 — Open project in VS Code
Open the folder:
```
C:\git\100_iot\git\mvk_iot_embedded\clock\005_clock_product\clock_product
```
> Open the `clock_product` folder — not the parent `005_clock_product`.

### Step 2 — Build (compile only, no flash)
**Via PlatformIO toolbar:** Click the checkmark (✓) icon  
**Via terminal:**
```bash
cd clock_product
pio run
```

Expected output:
```
RAM:   [=         ]  14.3% (used 46696 bytes from 327680 bytes)
Flash: [======    ]  61.7% (used 809069 bytes from 1310720 bytes)
========================= [SUCCESS] =========================
```

---

## 5. Flash (Upload) Steps

### Step 3 — Connect ESP32
- Plug ESP32 into PC via USB
- Check Device Manager (Windows) to confirm COM port (e.g. COM3)

### Step 4 — Upload firmware
**Via PlatformIO toolbar:** Click the right-arrow (→) upload icon  
**Via terminal:**
```bash
pio run --target upload
```

PlatformIO auto-detects the COM port. Expected output:
```
Auto-detected: COM3
...
Hash of data verified.
Hard resetting via RTS pin...
========================= [SUCCESS] =========================
```

> If upload fails with "port not found": check USB cable is data-capable, install CP210x/CH340 driver, verify COM port in Device Manager.

---

## 6. Verify Deployment

### Step 5 — Open Serial Monitor
**Via PlatformIO toolbar:** Click the plug icon  
**Via terminal:**
```bash
pio device monitor --port COM3 --baud 115200
```

Expected boot log:
```
RTC initialized.
Scanning networks... 12 networks found:
  Kiran_2.4G (-53 dBm)
  ...
Connecting to: Kiran_2.4G
.....
IP Address: 192.168.1.7
API server started on port 80
```

### Step 6 — Verify OLED
The OLED should display the name screen on boot. It rotates every 60 seconds:
```
Screen 0 (0s)  : "Vishnu / Kiran"
Screen 1 (60s) : Time HH:MM:SS + DD-MM-YYYY
Screen 2 (120s): "Temp / XX.X C"
```

### Step 7 — Verify API (optional)
From any browser on the same WiFi network:

| URL | Expected Response |
|---|---|
| `http://<IP>/hello` | `Hello World from ESP32!` |
| `http://<IP>/gettime` | `14:17:31  02-06-2026` |

Replace `<IP>` with the IP shown in the serial monitor.

---

## 7. Troubleshooting

| Symptom | Likely Cause | Fix |
|---|---|---|
| OLED blank | Wrong I2C address or wiring | Check SDA=GPIO21, SCL=GPIO22 |
| "RTC not found!" on serial | RTC wiring issue | Check RTC SDA/SCL/VCC/GND |
| WiFi fails after 20 attempts | Wrong SSID/password | Update config.h and reflash |
| Upload fails — port busy | Serial monitor still open | Close monitor, retry upload |
| Upload fails — no port found | Driver missing | Install CP210x or CH340 driver |
| Wrong time on display | RTC not set | Set `SET_TIME true`, flash, reset to false, reflash |

---

## 8. Re-flash Checklist

Before every flash:
- [ ] config.h WiFi credentials correct
- [ ] `SET_TIME false` (unless intentionally setting time)
- [ ] Serial monitor closed
- [ ] ESP32 connected via data USB cable
- [ ] Build succeeds with no errors
