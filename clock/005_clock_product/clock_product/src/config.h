#pragma once

// ── I2C Bus ─────────────────────────────────────
#define I2C_SDA          21
#define I2C_SCL          22
#define I2C_CLOCK        100000

// ── Display ──────────────────────────────────────
#define SWITCH_INTERVAL  60000   // ms between screen changes

// ── WiFi ─────────────────────────────────────────
#define WIFI_SSID        "Kiran_2.4G"
#define WIFI_PASSWORD    "9966099818"

// ── RTC Time Set ─────────────────────────────────
// Set to true once to program RTC, then back to false
#define SET_TIME         false
#define T_YEAR           2026
#define T_MONTH          5
#define T_DAY            2
#define T_HOUR           11
#define T_MINUTE         36
#define T_SECOND         0
