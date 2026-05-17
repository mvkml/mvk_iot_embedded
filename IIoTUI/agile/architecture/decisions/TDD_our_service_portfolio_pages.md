# 🏗️ Technical Design Document (TDD)
# Pages: Our Service + Portfolio
# Author: Architect Agent
# Date: 2026-05-03
# Version: 1.0

## Component Structure
- `features/our-service/our-service.ts/.html/.scss`
- `features/portfolio/portfolio.ts/.html/.scss`
- Shared Navbar reused

## Routes
- `/services` → OurServiceComponent (SSR)
- `/portfolio` → PortfolioComponent (SSR)

## Rendering: SSR (public pages)
## Styling: SCSS — same theme variables
## API: None (static content)
