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

static void handleSetTime() {
  if (!_server->hasArg("year")   || !_server->hasArg("month") ||
      !_server->hasArg("day")    || !_server->hasArg("hour")  ||
      !_server->hasArg("minute") || !_server->hasArg("second")) {
    _server->send(400, "text/plain",
      "Missing params. Use: /settime?year=2026&month=6&day=2&hour=14&minute=30&second=0");
    return;
  }

  int y  = _server->arg("year").toInt();
  int mo = _server->arg("month").toInt();
  int d  = _server->arg("day").toInt();
  int h  = _server->arg("hour").toInt();
  int mi = _server->arg("minute").toInt();
  int s  = _server->arg("second").toInt();

  _rtc->adjust(DateTime(y, mo, d, h, mi, s));

  char response[60];
  sprintf(response, "Time set to %02d:%02d:%02d  %02d-%02d-%04d", h, mi, s, d, mo, y);
  _server->send(200, "text/plain", response);
}

void initApiService(WebServer &server, RTC_DS3231 &rtc) {
  _server = &server;
  _rtc    = &rtc;
  server.on("/hello",   handleHello);
  server.on("/gettime", handleGetTime);
  server.on("/settime", handleSetTime);
}
