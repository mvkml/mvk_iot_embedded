#include <Arduino.h>
#include <Wire.h>
#include <WiFi.h>
#include <WebServer.h>
#include "config.h"
#include "display_module.h"
#include "rtc_module.h"
#include "wifiService.h"
#include "apiService.h"

WebServer server(80);

int displayState = 0;           // 0=name, 1=time, 2=temp
unsigned long lastSwitch = 0;

void setup() {
    Serial.begin(115200);

    Wire.begin(I2C_SDA, I2C_SCL);
    Wire.setClock(I2C_CLOCK);

    initDisplay();

    if (!initRTC()) {
        while (true);
    }

    setupWifi(WIFI_SSID, WIFI_PASSWORD);

    if (WiFi.status() == WL_CONNECTED) {
        Serial.println("\n================================");
        Serial.println("\n================================");
        Serial.println("\n================================");
        Serial.println("\n================================");
        Serial.println("\n================================");
        initApiService(server, rtc);
        server.begin();
        String ip = WiFi.localIP().toString();
        Serial.println("  WiFi Connected!");
        Serial.print("  URL: http://");
        Serial.println(ip);
        Serial.print("  GET http://"); Serial.print(ip); Serial.println("/hello");
        Serial.print("  GET http://"); Serial.print(ip); Serial.println("/gettime");
        Serial.println("================================\n");
        displayIP(ip.c_str());
        delay(5000);
    }

    lastSwitch = millis();
}

void loop() {
    if (WiFi.status() == WL_CONNECTED) {
        server.handleClient();
    }

    if (millis() - lastSwitch >= SWITCH_INTERVAL) {
        displayState = (displayState + 1) % 3;
        lastSwitch = millis();
    }

    switch (displayState) {
        case 0: displayName(); break;
        case 1: displayTime(); break;
        case 2: displayTemp(); break;
    }

    delay(1000);
}
