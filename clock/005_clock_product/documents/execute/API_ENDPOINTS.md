# API Endpoints — ESP32 Clock Product

**Base URL:** `http://<IP>` (IP shown in serial monitor on boot)  
**Port:** 80  
**Source:** `clock_product/src/apiService.cpp`

---

## Endpoint List

| # | Endpoint | Method | Description |
|---|---|---|---|
| 1 | `/hello` | GET | Health check |
| 2 | `/gettime` | GET | Read current time and date from RTC |
| 3 | `/settime` | GET | Set RTC time via query parameters |

---

## Endpoint Details

### 1. GET `/hello`

Health check — confirms the ESP32 API server is reachable.

**Request:**
```
http://192.168.1.7/hello
```

**Response:**
```
Hello World from ESP32!
```

---

### 2. GET `/gettime`

Returns the current time and date read from the DS3231 RTC.

**Request:**
```
http://192.168.1.7/gettime
```

**Response:**
```
14:17:31  02-06-2026
```

**Format:** `HH:MM:SS  DD-MM-YYYY`

---

### 3. GET `/settime`

Sets the DS3231 RTC time. All 6 parameters are required.

**Request:**
```
http://192.168.1.7/settime?year=2026&month=6&day=2&hour=14&minute=30&second=0
```

**Query Parameters:**

| Parameter | Type | Example | Description |
|---|---|---|---|
| `year` | int | `2026` | 4-digit year |
| `month` | int | `6` | Month (1–12) |
| `day` | int | `2` | Day (1–31) |
| `hour` | int | `14` | Hour (0–23, 24h format) |
| `minute` | int | `30` | Minute (0–59) |
| `second` | int | `0` | Second (0–59) |

**Response (success):**
```
Time set to 14:30:00  02-06-2026
```

**Response (missing params):**
```
Missing params. Use: /settime?year=2026&month=6&day=2&hour=14&minute=30&second=0
```

---

## Quick Reference

```
GET http://<IP>/hello
GET http://<IP>/gettime
GET http://<IP>/settime?year=YYYY&month=MM&day=DD&hour=HH&minute=MM&second=SS
```

---

## How to Get IP Address

1. Flash firmware and open serial monitor: `pio device monitor`
2. Look for:
   ```
   URL: http://192.168.1.7
   ```
3. The IP is also shown on the OLED display for 5 seconds on every boot.
