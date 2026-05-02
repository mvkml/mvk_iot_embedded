#pragma once
#include <WebServer.h>
#include <RTClib.h>

void initApiService(WebServer &server, RTC_DS3231 &rtc);
