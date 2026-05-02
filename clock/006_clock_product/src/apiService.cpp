#include "apiService.h"
#include <Arduino.h>

static WebServer*   _server;
static RTC_DS3231*  _rtc;

static void handleHello() {
  _server->send(200, "text/plain", "Hello World from ESP32!");
}

static void handleGetTime() {
  DateTime now = _rtc->now();

  char response[40];
  sprintf(response, "%02d:%02d:%02d  %02d-%02d-%04d",
    now.hour(), now.minute(), now.second(),
    now.day(), now.month(), now.year()
  );

  _server->send(200, "text/plain", response);
}

void initApiService(WebServer &server, RTC_DS3231 &rtc) {
  _server = &server;
  _rtc    = &rtc;
  server.on("/hello",   handleHello);
  server.on("/gettime", handleGetTime);
}
