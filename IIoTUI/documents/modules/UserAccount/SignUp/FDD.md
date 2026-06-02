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
                    └─── User ID is unique → Save to DB → Show success message + "Go to Login →" link
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
| PIN | ✅ | Numeric (hidden, max 4) | 4-digit quick-access PIN for PIN login |
| Confirm PIN | ✅ | Numeric (hidden, max 4) | Must match PIN |
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
| PIN | Exactly 4 digits, all numeric | "PIN must be exactly 4 digits." |
| Confirm PIN | Must match PIN | "PINs do not match." |
| PIN | Must be unique in DB | "PIN already in use. Please choose a different PIN." |
| Account Type | Must be selected | "Please select an account type." |

---

## Success Behaviour

- Record saved to `UserAccount` table
- `IsActive` set to `true`
- `CreatedDate` and `UpdatedDate` set to current timestamp
- Success message displayed in green: *"Sign-up successful!"*
- "Go to Login →" link appears below the message — tapping it navigates to Login page

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
| Pin | From form (4-digit string) |
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
