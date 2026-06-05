# ADR-0001: Monorepo Architecture, Stack, and Philosophy

- **Status:** Accepted
- **Date:** 2026-06-05
- **Deciders:** Terentiy Gatsukov
- **Replaces:** N/A (initial)

---

## Context

This repository exists as a **code-as-portfolio** artifact — a public demonstration of full-stack
engineering judgment. Every artifact (code, configuration, documentation) is written to be read,
evaluated, and learned from by peers, hiring managers, and my future self.

The repository must:

1. **Demonstrate breadth** across the stack — backend, frontend, infrastructure, and documentation.
2. **Demonstrate depth** within each layer — not toys, but production-pattern code.
3. **Be self-documenting** — architecture decisions, runbooks, and API specs live alongside code.
4. **Be a realistic simulation** of a team-scale monorepo, even though authored by one person.

## Decision

### Monorepo

All components live in a single repository under a flat directory structure (`/backend`, `/frontend`,
`/infra`, `/docs`). This is chosen over a polyrepo for three reasons:

- **Atomic commits** across stack layers (e.g., a feature that spans API + UI + migration).
- **Single source of truth** for shared contracts (protobuf schemas, OpenAPI specs, config).
- **Portfolio clarity** — one URL, one coherent narrative.

Polyrepo would be preferred if independent deploy cadences and team boundaries existed; neither applies
here.

### Backend: Go

Go is chosen for the backend services. Rationale:

- **Readability.** Go's simplicity makes code review straightforward — critical for a portfolio.
- **Standard library.** Production-grade HTTP server, TLS, and testing with zero framework lock-in.
- **Concurrency model.** Goroutines and channels demonstrate systems-thinking without ceremony.
- **Single binary deploy.** Simplifies infra and CI — one artifact per service.

Alternatives considered: TypeScript/Node.js (strong ecosystem, but concurrency story weaker for
portfolio depth); Rust (excellent, but higher cognitive overhead for readers); Python (ubiquitous but
less interesting for systems patterns).

### Frontend: TypeScript, React, Next.js

TypeScript + React + Next.js is the industry baseline for web applications. This stack is chosen
because:

- **Type safety** end-to-end when paired with generated API clients from backend contracts.
- **Next.js App Router** demonstrates modern React patterns (Server Components, streaming).
- **Tailwind CSS** for utility-first styling — fast iteration, design-system friendly.
- **Portfolio relevance** — this is the stack most engineering teams evaluate against today.

Alternatives considered: Vue/Nuxt (excellent DX, smaller hiring surface); SvelteKit (elegant, but
smaller ecosystem).

### Infrastructure: Terraform + Docker

Infrastructure is defined as code from day one:

- **Terraform** for cloud resource provisioning (provider TBD — likely AWS or Fly.io).
- **Docker** for service containerization and local development parity.
- **GitHub Actions** for CI/CD — public repo, generous free tier, tight GitHub integration.

Alternatives considered: Pulumi (more flexible, less ubiquitous); Kubernetes (over-engineered for this
scale — Docker Compose + Terraform is sufficient).

### Data: PostgreSQL + Redis

- **PostgreSQL** as the primary relational database — mature, feature-rich, and the default correct
  choice for most applications.
- **Redis** for caching, session state, and job queues — complements PostgreSQL's durability with
  in-memory speed.

### Communication Patterns

- **External:** REST (OpenAPI 3.1) for public API surface — universal, cacheable, tooling-rich.
- **Internal:** gRPC or message-based (NATS/Redis Streams) for service-to-service — added when
  services multiply.
- **Contracts-first:** API schemas are written before implementation and live in `/docs/api/`.

### Documentation

Architecture Decision Records (ADRs) are the primary mechanism for capturing technical decisions.
Each ADR is:

- **Immutable** once accepted — superseded by a new ADR, never edited.
- **Numbered sequentially** — `NNNN-title-with-dashes.md`.
- **Stored alongside code** in `/docs/adr/` — not in a wiki or external tool.

## Consequences

### Positive

- The monorepo tells a complete story — a visitor can trace a feature from Terraform to CSS.
- Go + TypeScript provide strong typing across the stack, reducing integration surprises.
- ADRs create a durable record of _why_ choices were made, not just _what_ was built.
- Conventional Commits make the git history machine-readable and changelog-friendly.

### Negative / Trade-offs

- Monorepo tooling (build caching, affected-pipeline detection) adds initial setup complexity.
- Go's verbosity in error handling may feel repetitive — intentional, but acknowledged.
- Single-contributor "team" means branching strategies and code review are simulated, not organic.
- ADRs add upfront writing cost — worth it for the portfolio goal, but heavier than a typical solo
  project.

---

_This ADR is the root of the decision tree. Future ADRs will reference it as they refine specific
choices: database schema design, API versioning, authentication strategy, deployment targets, and
observability stack._
