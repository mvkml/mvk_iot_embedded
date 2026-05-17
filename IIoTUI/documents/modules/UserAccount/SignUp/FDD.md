# Sign Up — Functional Design Document

**Module:** UserAccount  
**Sub-Module:** Sign Up  
**Version:** v1  
**Date:** 2026-05-17  
**Status:** Active  

---

## Purpose

Allow a new user to register an account in the app. On successful signup the user's record is saved to the local SQLite database and the user is informed of success.

---

## User Flow

```
User opens Sign Up page
        │
        ▼
Fills in the form fields
        │
        ▼
Taps "Sign Up" button
        │
        ├─── Validation fails → Show error message (stay on page)
        │
        └─── Validation passes
                    │
                    ├─── User ID already exists → Show error message
                    │
                    └─── User ID is unique → Save to DB → Show success alert → Stay on page
```

---

## Form Fields

| Field | Required | Input Type | Description |
|-------|----------|------------|-------------|
| User ID | ✅ | Text (email / phone) | Unique login identifier |
| Name | ✅ | Text | Display name |
| Password | ✅ | Password (hidden) | Login password |
| Confirm Password | ✅ | Password (hidden) | Must match Password |
| Account Type | ✅ | Picker | Selected from UserType list (User / Admin) |
| Description | ☐ | Text | Optional — user bio or note |

---

## Validation Rules

| Field | Rule | Error Message |
|-------|------|---------------|
| User ID | Not empty | "Please enter a User ID (phone number or email)." |
| User ID | Must be unique in DB | "User ID already exists. Please use a different one." |
| Name | Not empty | "Please enter your name." |
| Password | Not empty | "Please enter a password." |
| Confirm Password | Must match Password | "Passwords do not match." |
| Account Type | Must be selected | "Please select an account type." |

---

## Success Behaviour

- Record saved to `UserAccount` table
- `IsActive` set to `true`
- `CreatedDate` and `UpdatedDate` set to current timestamp
- Success alert displayed: *"Your account has been created successfully!"*
- Form stays on Sign Up page (user navigates back manually)

---

## Error Behaviour

- Error message shown below the form in red
- Form stays on the same page — user corrects and retries

---

## Data Written to DB

| Column | Value |
|--------|-------|
| UserId | From form |
| Name | From form |
| Password | From form (plain text — ADR 003 pending) |
| Description | From form (empty string if blank) |
| IsActive | true |
| UserTypeId | From selected UserType |
| CreatedDate | DateTime.Now |
| UpdatedDate | DateTime.Now |

---

## Out of Scope (v1)

- Email format validation
- Phone number format validation
- Password strength rules
- Email / OTP verification
- Password hashing (tracked in ADR 003)
