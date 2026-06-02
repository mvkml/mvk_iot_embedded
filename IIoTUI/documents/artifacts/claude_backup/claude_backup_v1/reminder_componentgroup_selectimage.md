---
name: reminder-componentgroup-selectimage
description: Pending backlog item CG-001 — SelectImagePage must pre-filter image type category when opened from ComponentGroup picker; remind on any ComponentGroup work
metadata: 
  node_type: memory
  type: project
  originSessionId: 9237c530-8105-4b86-a5eb-8be1d5b93014
---

**CG-001 — SelectImagePage image type pre-filter (NOT yet implemented)**

When the user taps `+` for Default / Disable / Background image on ManageComponentGroupPage,
the SelectImagePage should automatically pre-filter its category dropdown to the matching
image type (default / disable / background), not show "All Categories".

**Why:** Saves the user from having to manually filter; the caller (`targetField`) already
knows which type is being picked — the `categoryId` just needs to match the right category.

**How to apply:** Whenever any ComponentGroup feature is being worked on, remind the user
of this pending item and reference `documents/modules/ComponentGroup/BACKLOG_v2.md` item CG-001.

**What's already done:**
- `SelectImageViewModel` already reads `categoryId` from query params (`_initialCategoryId`)
- `preSelectedImageId` is already passed from ManageComponentGroupViewModel

**What still needs doing:**
- Identify the correct `CategoryId` values for default/disable/background image types
- Pass the correct `categoryId` (not `"0"`) from each Pick command in ManageComponentGroupViewModel
