# US002 — Screen Alternation

**Sprint:** Sprint 01
**Status:** ✅ Done
**Priority:** High

## User Story
As a user, I want the display to automatically rotate between screens every minute, so I can see both my name and the current time without pressing any button.

## Acceptance Criteria
- [ ] Screen switches from name to time every 60 seconds
- [ ] Screen switches from time back to name after another 60 seconds
- [ ] Switching uses `millis()` non-blocking timer (no `delay()` for rotation)
- [ ] Display refreshes every 1 second to update the time
- [ ] Clock continues running when the name screen is shown

## Tasks
| ID | Title |
|----|-------|
| TASK006 | Implement screen alternation (millis() timer, SWITCH_INTERVAL=60000) |

## Implementation Notes
- `SWITCH_INTERVAL` defined as 60000ms (1 minute)
- `showName` bool flag controls current screen
- `lastSwitch` stores `millis()` at last screen change
- `delay(1000)` in loop() refreshes display every second
