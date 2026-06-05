# ADR-0002: Actual Stack — .NET 10, React 19, PixiJS

- **Status:** Accepted
- **Date:** 2026-06-05
- **Deciders:** Terentiy Gatsukov
- **Supersedes:** ADR-0001 (backend and frontend sections)

---

## Context

ADR-0001 was written at repo initialization with provisional stack choices. The portfolio has since
taken shape with concrete implementation. This ADR records the actual stack, verified against the
repository's `Directory.Packages.props`, `global.json`, and Docker Compose.

The portfolio owner is a Senior .NET Backend Developer with ~5 years of C# experience, deep iGaming
domain expertise, and a public GitHub profile (lilter96). The stack must authentically represent
this background while demonstrating full-stack range.

## Decision

### Backend: .NET 10 (LTS), C#, Minimal APIs

**.NET 10 on ASP.NET Core Minimal APIs** is the backend platform. Rationale:

- **Authenticity.** The author's primary professional identity is .NET. Choosing it over Go
  (ADR-0001's provisional choice) ensures the code reads as genuine expertise, not a learning
  exercise.
- **Minimal APIs with typed results.** Endpoint groups (`MapGroup`), `TypedResults<T>`, and
  endpoint filters provide a modern, low-ceremony API surface without the weight of MVC.
- **Vertical Slice architecture.** `/Api`, `/Application`, `/Domain`, `/Infrastructure` with
  clean dependency arrows: Api → Application + Infrastructure, Application → Domain,
  Infrastructure → Application.
- **iGaming centerpiece.** The provably-fair slot demo requires HMAC-SHA256, cryptographic RNG,
  and deterministic verification — all natural in .NET.
- **Ecosystem.** EF Core 10 with PostgreSQL JSONB for reel strips/paytables, Serilog for
  structured logging, FluentValidation for request validation, Scalar for OpenAPI docs.

Verified runtime: .NET SDK 10.0.300, EF Core Tools 10.0.0.

### Frontend: React 19 + TypeScript + Vite

React 19 with TypeScript (strict mode) and Vite for the frontend. Rationale:

- **Vite over Next.js.** No SSR requirement for a portfolio; Vite's fast HMR and simple build
  are better suited. The slot demo lives entirely client-side with PixiJS.
- **TanStack Query v5** for server-state management — caching, refetching, and optimistic
  updates with minimal boilerplate.
- **react-i18next** for EN/RU bilingual support — the author is based in Belarus and the
  portfolio is bilingual.
- **Motion** (formerly Framer Motion) for React animation — page transitions, scroll reveals.
- **PixiJS v8** for the interactive slot renderer — the signature demo that proves iGaming
  domain expertise with real graphics, not just API responses.

### Interactive Demo: PixiJS v8 Slot Renderer

The portfolio's centerpiece is a provably-fair slot machine. PixiJS v8 is the renderer
because:

- **Canvas/WebGL performance.** Slot reel animations require 60fps sprite rendering with
  easing curves — PixiJS handles this natively.
- **iGaming authenticity.** Real slot games use canvas/WebGL renderers, not DOM/CSS.
  PixiJS demonstrates domain-appropriate tool selection.
- **Integration with .NET provably-fair backend.** The frontend sends a client seed, the
  backend returns a spin result with HMAC proof, and PixiJS animates the outcome.

### Data: PostgreSQL 17 + Redis 7

- **PostgreSQL 17** (Docker: `postgres:17-alpine`) — JSONB columns for reel strips, paytable
  definitions, and spin history; relational model for users, projects, experience.
- **Redis 7** (Docker: `redis:7-alpine`) — via `StackExchangeRedis` for caching and rate
  limiting.

### Infrastructure: Docker Compose + GitHub Actions

- **Local dev:** `docker compose -f infra/docker-compose.yml up -d` (PostgreSQL + Redis).
- **CI/CD:** GitHub Actions (public repo, free tier).

## Consequences

### Positive

- .NET + React authentically represents the author's professional identity and 5 years of
  C# experience.
- The slot demo with PixiJS is a unique, domain-authentic portfolio centerpiece that no
  generic portfolio template can replicate.
- Centralized package management (`Directory.Packages.props`) locks exact versions and
  simplifies auditing.
- TreatWarningsAsErrors + AnalysisMode=All enforces a senior-level code quality bar.

### Negative / Trade-offs

- .NET 10 tooling requires SDK 10.0.x installed; Docker Compose requires Docker daemon.
- PixiJS v8 is a significant dependency for a single feature; the slot demo must carry its
  weight.
- The Go decision in ADR-0001 was reasonable at the time but inauthentic to the author's
  actual expertise.

---

## Verified Dependency Versions (2026-06-05)

| Package | Version |
|---------|---------|
| .NET SDK | 10.0.300 |
| EF Core Tools | 10.0.0 |
| Npgsql.EntityFrameworkCore.PostgreSQL | 10.0.0 |
| Serilog.AspNetCore | 9.0.0 |
| Scalar.AspNetCore | 2.1.0 |
| Asp.Versioning.Http | 8.1.0 |
| FluentValidation | 12.0.0 |
| xunit | 2.9.3 |
| PostgreSQL (Docker) | 17-alpine |
| Redis (Docker) | 7-alpine |
