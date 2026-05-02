#include <Arduino.h>
#include <Wire.h>
#include <U8g2lib.h>
#include <RTClib.h>
#include <WiFi.h>
#include <WebServer.h>
#include "apiService.h"
#include "wifiService.h"

// ── Set your time here ──────────────────────────
#define SET_TIME   false
#define T_YEAR     2026
#define T_MONTH    5
#define T_DAY      2
#define T_HOUR     11
#define T_MINUTE   36
#define T_SECOND   0
// ────────────────────────────────────────────────

// ── WiFi credentials ────────────────────────────
const char* ssid     = "VishnuKiran";
const char* password = "8523007299";
// ────────────────────────────────────────────────

#define SWITCH_INTERVAL  60000

const char* firstName = "Thank You";
const char* lastName  = "Vidhya";

U8G2_SH1106_128X64_NONAME_F_HW_I2C u8g2(U8G2_R0, U8X8_PIN_NONE);
RTC_DS3231 rtc;
WebServer server(80);

bool showName = true;
unsigned long lastSwitch = 0;
unsigned long lastDisplay = 0;

void displayName() {
  u8g2.clearBuffer();
  u8g2.setFont(u8g2_font_helvB14_tf);
  u8g2.drawStr(0, 24, firstName);
  u8g2.drawStr(0, 50, lastName);
  u8g2.sendBuffer();
}

void displayTime() {
  DateTime now = rtc.now();

  char buf[20];
  sprintf(buf, "%02d:%02d:%02d", now.hour(), now.minute(), now.second());
  char dateBuf[20];
  sprintf(dateBuf, "%02d-%02d-%04d", now.day(), now.month(), now.year());

  u8g2.clearBuffer();
  u8g2.setFont(u8g2_font_helvB14_tf);
  u8g2.drawStr(0, 24, buf);
  u8g2.setFont(u8g2_font_helvB10_tf);
  u8g2.drawStr(0, 50, dateBuf);
  u8g2.sendBuffer();
}

void setup() {
  Serial.begin(115200);

  Wire.begin(21, 22);
  Wire.setClock(100000);

  u8g2.setBusClock(100000);
  u8g2.begin();

  if (!rtc.begin()) {
    Serial.println("RTC not found!");
    while (true);
  }

  if (SET_TIME) {
    rtc.adjust(DateTime(T_YEAR, T_MONTH, T_DAY, T_HOUR, T_MINUTE, T_SECOND));
    Serial.println("Time set!");
  }

  Serial.println("RTC initialized");

  setupWifi(ssid, password);

  initApiService(server, rtc);
  server.begin();
  Serial.println("Server started");

  lastSwitch = millis();
  lastDisplay = millis();
}

void loop() {
  server.handleClient();

  if (millis() - lastSwitch >= SWITCH_INTERVAL) {
    showName = !showName;
    lastSwitch = millis();
  }

  if (millis() - lastDisplay >= 1000) {
    if (showName) {
      displayName();
    } else {
      displayTime();
    }
    lastDisplay = millis();
  }
}
