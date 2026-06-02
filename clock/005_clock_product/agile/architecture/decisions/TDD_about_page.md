# 🏗️ Technical Design Document (TDD)
# Page: About Page
# Author: Architect Agent
# Date: 2026-05-03
# Version: 1.0
# Status: Draft

---

## 1. Component Structure

```
src/app/
├── features/
│   └── about/
│       ├── about.ts               ← Standalone component
│       ├── about.html             ← Template
│       └── about.scss             ← Component styles
└── shared/
    └── components/
        └── navbar/
            ├── navbar.ts          ← Shared navbar (reused from landing)
            ├── navbar.html
            └── navbar.scss
```

### Component Breakdown

| Component | Type | Description |
|-----------|------|-------------|
| `AboutComponent` | Standalone | Main about page container |
| `NavbarComponent` | Shared Standalone | Reused navbar from landing page |
| Hero Banner | Inline section | Static HTML inside about.html |
| Feature Cards | Inline section | 4 cards in about.html |
| Tech Stack Section | Inline section | Technology highlights |
| Team Section | Inline section | Agent/team cards |
| Footer CTA | Inline section | Sign up call to action |

---

## 2. Rendering Type

| Type | Decision | Reason |
|------|----------|--------|
| **SSR** | ✅ Yes | Public page — fast load + SEO |
| CSR | ❌ No | Not needed for static content |

---

## 3. Routing

```typescript
// app.routes.ts
{ path: 'about', component: AboutComponent }
```

| Route | Component | Rendering |
|-------|-----------|-----------|
| `/` | LandingComponent | SSR |
| `/about` | AboutComponent | SSR |

---

## 4. Styling Approach

### SCSS Variables (shared theme)
```scss
$bg-dark:    #0a0e2a;   // Page background
$bg-card:    #0d1240;   // Card background
$purple:     #7C6FFF;   // Primary accent
$blue:       #4fc3f7;   // Secondary accent
$white:      #ffffff;   // Text
$text-muted: #a0aec0;   // Muted text
$gradient: linear-gradient(135deg, #7C6FFF, #4fc3f7);
```

### Layout
- Flexbox for navbar and feature cards
- CSS Grid for team section
- Max-width container: 1200px
- Responsive breakpoints: 768px, 1024px

---

## 5. Assets Required

| Asset | Type | Source |
|-------|------|--------|
| UC Robot icon | SVG / CSS | Reuse from landing page |
| Feature icons | Emoji / SVG | Inline |
| Tech icons | Emoji / SVG | Inline |
| Team avatars | CSS generated | Role-based colored circles |
| Background gradient | CSS | Pure CSS |

---

## 6. API Dependencies

| API | Required | Reason |
|-----|----------|--------|
| None | — | About page is fully static |

> ✅ No API calls needed for About page.

---

## 7. Reusable Components

| Component | Action | Notes |
|-----------|--------|-------|
| Navbar | 🆕 Create shared component | Extract from landing page |
| Feature Cards | ♻️ Similar to landing features strip | Slight variation |
| Footer CTA | 🆕 New | Sign Up button |

---

## 8. Performance

| Optimization | Applied | Detail |
|-------------|---------|--------|
| SSR | ✅ | Pre-rendered on server |
| Lazy Loading | ❌ | Not needed — SSR page |
| Image Optimization | ✅ | SVG/CSS only — no heavy images |
| CSS Minification | ✅ | Angular build handles this |

---

## 9. Accessibility

| Item | Requirement |
|------|-------------|
| Semantic HTML | Use `<section>`, `<nav>`, `<article>`, `<h1>`-`<h3>` |
| ARIA labels | Add to buttons and icons |
| Color contrast | Min 4.5:1 ratio (white on dark navy) |
| Keyboard navigation | Tab-navigable navbar and CTA button |

---

## 10. Browser Compatibility

| Browser | Supported |
|---------|-----------|
| Chrome (latest) | ✅ |
| Firefox (latest) | ✅ |
| Edge (latest) | ✅ |
| Safari (latest) | ✅ |
| IE 11 | ❌ Not supported |

---

## 11. Implementation Steps

| # | Step | Owner |
|---|------|-------|
| 1 | Extract Navbar into shared component | Dev Angular |
| 2 | Create `about/` feature folder | Dev Angular |
| 3 | Add `/about` route in app.routes.ts | Dev Angular |
| 4 | Implement about.html sections | Dev Angular |
| 5 | Implement about.scss styling | Dev Angular |
| 6 | Test SSR rendering | Dev DevOps |
| 7 | Review and approve | Product Owner |

---

*Approved by: Architect Agent | Date: 2026-05-03*
