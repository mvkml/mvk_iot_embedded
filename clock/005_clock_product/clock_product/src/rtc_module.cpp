#include "rtc_module.h"
#include "config.h"
#include <Arduino.h>

RTC_DS3231 rtc;

bool initRTC() {
    if (!rtc.begin()) {
        Serial.println("RTC not found!");
        return false;
    }

    if (SET_TIME) {
        rtc.adjust(DateTime(T_YEAR, T_MONTH, T_DAY, T_HOUR, T_MINUTE, T_SECOND));
        Serial.println("RTC time set.");
    }

    Serial.println("RTC initialized.");
    return true;
}

float getRTCTemperature() {
    return rtc.getTemperature();
}
