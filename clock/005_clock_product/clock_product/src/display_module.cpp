#include "display_module.h"
#include "rtc_module.h"
#include "config.h"
#include <Arduino.h>

U8G2_SH1106_128X64_NONAME_F_HW_I2C u8g2(U8G2_R0, U8X8_PIN_NONE);

void initDisplay() {
    u8g2.setBusClock(I2C_CLOCK);
    u8g2.begin();
}

void displayName() {
    u8g2.clearBuffer();
    u8g2.setFont(u8g2_font_helvB14_tf);
    u8g2.drawStr(0, 24, "Vishnu");
    u8g2.drawStr(0, 50, "Kiran");
    u8g2.sendBuffer();
}

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

void displayIP(const char* ip) {
    u8g2.clearBuffer();
    u8g2.setFont(u8g2_font_helvB10_tf);
    u8g2.drawStr(0, 20, "IP Address:");
    u8g2.drawStr(0, 40, ip);
    u8g2.setFont(u8g2_font_6x10_tf);
    u8g2.drawStr(0, 58, "port 80");
    u8g2.sendBuffer();
}
