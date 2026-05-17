# 📋 Functional Design Document (FDD)
# Page: About Page
# Author: Product Owner Agent
# Date: 2026-05-03
# Version: 1.0
# Status: Draft

---

## 1. Purpose
The About page introduces UC — Your Copilot to users.
It communicates the product vision, mission, what it does,
the technology behind it, and the team that built it.
It is a public-facing page accessible before login.

---

## 2. Scope

### In Scope
- Static informational content about UC
- Navigation to other public pages
- Call to action (Sign Up / Get Started)

### Out of Scope
- User authentication
- Dynamic data from API
- HR-specific features

---

## 3. Target Users

| User Type | Description |
|-----------|-------------|
| Visitor | Anyone who lands on the About page |
| Potential Client | HR manager evaluating UC |
| New User | Someone considering Sign Up |

---

## 4. Page Sections

| # | Section | Content | Purpose |
|---|---------|---------|---------|
| 1 | **Navbar** | Logo, Home, About, Our Service, Portfolio, Contact Us, Log In, Sign Up | Navigation |
| 2 | **Hero Banner** | Title: "About UC", Tagline: "Your Intelligent HR Partner" | First impression |
| 3 | **Who We Are** | Short intro paragraph about UC — Your Copilot | Brand identity |
| 4 | **Our Mission** | Mission statement — helping HR teams work smarter | Vision |
| 5 | **What We Do** | 4 feature cards: Smart Search, Deep Insights, Accurate Results, Secure & Private | Value proposition |
| 6 | **Our Technology** | Highlights: Angular, FastAPI, Azure AI Search, Azure Storage | Trust & credibility |
| 7 | **Our Team** | Agent roles: Architect, Product Owner, Dev Angular, Dev FastAPI, Dev SQL, Dev DevOps | Team showcase |
| 8 | **Footer CTA** | "Ready to get started?" + Sign Up button | Conversion |

---

## 5. Navigation Flow

```
Landing Page (/)
      ↓
About Page (/about)
      ↓
Sign Up Page (/signup)  OR  Contact Page (/contact)
```

---

## 6. Content Requirements

| Section | Text | Image/Icon |
|---------|------|------------|
| Hero | "About UC — Your Copilot" | Background gradient |
| Who We Are | 3-4 sentences | UC Robot icon |
| Our Mission | 2-3 sentences | Mission icon |
| What We Do | 4 cards with title + description | 4 icons |
| Our Technology | Tech name + short desc | Tech logos/icons |
| Our Team | Name + role + short bio | Avatar/icon per agent |
| Footer CTA | 1 line + button | — |

---

## 7. Acceptance Criteria

| # | Criteria | Verified By |
|---|----------|-------------|
| AC1 | All 8 sections visible on page | Product Owner |
| AC2 | Navbar matches landing page style | Product Owner |
| AC3 | Page is responsive on desktop | Dev Angular |
| AC4 | "Sign Up" button navigates correctly | Dev Angular |
| AC5 | Dark navy theme consistent with landing | Architect |
| AC6 | Page loads fast (SSR) | Dev DevOps |

---

*Approved by: Product Owner Agent | Date: 2026-05-03*
