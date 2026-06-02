# Execution Guide — ESP32 Clock Product

**Purpose:** Step-by-step commands to build, flash, and verify the clock firmware.

---

## Step 1 — Open Terminal

Open terminal in VS Code and navigate to the PlatformIO project folder:

```bash
cd C:\git\100_iot\git\mvk_iot_embedded\clock\005_clock_product\clock_product
```

---

## Step 2 — Build (Compile)

```bash
pio run
```

Expected output:
```
RAM:   [=         ]  14.3% (used 46696 bytes from 327680 bytes)
Flash: [======    ]  61.9% (used 811701 bytes from 1310720 bytes)
========================= [SUCCESS] =========================
```

---

## Step 3 — Flash (Upload to ESP32)

Connect ESP32 via USB, then run:

```bash
pio run --target upload
```

Expected output:
```
Auto-detected: COM3
...
Hash of data verified.
Hard resetting via RTS pin...
========================= [SUCCESS] =========================
```

---

## Step 4 — Open Serial Monitor

```bash
pio device monitor
```

Expected boot output:
```
RTC initialized.
Scanning networks... 12 networks found:
Connecting to: Kiran_2.4G
.....

================================
  WiFi Connected!
  URL: http://192.168.1.7
  GET http://192.168.1.7/hello
  GET http://192.168.1.7/gettime
================================
```

Press `Ctrl+C` to exit the serial monitor.

---

## Step 5 — Get IP Address

IP address is printed in the serial monitor after WiFi connects:
```
URL: http://192.168.1.7
```

It is also shown on the OLED display for 5 seconds on every boot.

---

## Step 6 — Test API Endpoints

Open a browser and paste the URLs:

| URL | Expected Response |
|---|---|
| `http://192.168.1.7/hello` | `Hello World from ESP32!` |
| `http://192.168.1.7/gettime` | `14:17:31  02-06-2026` |

> Replace `192.168.1.7` with the actual IP shown in serial monitor.

---

## Step 7 — OLED Screen Rotation

After the IP screen disappears (5 seconds), the OLED rotates every 60 seconds:

| Screen | Content |
|---|---|
| 0 | Name — Vishnu / Kiran |
| 1 | Time HH:MM:SS + Date DD-MM-YYYY |
| 2 | Temperature in Celsius |

---

## One-Line: Upload + Monitor Together

```bash
pio run --target upload && pio device monitor
```

---

## Troubleshooting

| Problem | Fix |
|---|---|
| Upload fails — port busy | Close serial monitor first, then retry upload |
| No IP in serial output | Check WiFi credentials in `config.h` |
| OLED blank | Check I2C wiring: SDA=GPIO21, SCL=GPIO22 |
| Wrong time on display | Set `SET_TIME true` in `config.h`, flash, then set back to `false` and reflash |
