# Portfolio

[![Backend CI](https://github.com/lilter96/portfolio/actions/workflows/backend-ci.yml/badge.svg)](https://github.com/lilter96/portfolio/actions/workflows/backend-ci.yml)
[![Frontend CI](https://github.com/lilter96/portfolio/actions/workflows/frontend-ci.yml/badge.svg)](https://github.com/lilter96/portfolio/actions/workflows/frontend-ci.yml)
[![Docker Build](https://github.com/lilter96/portfolio/actions/workflows/docker-build.yml/badge.svg)](https://github.com/lilter96/portfolio/actions/workflows/docker-build.yml)
[![Deploy Frontend](https://github.com/lilter96/portfolio/actions/workflows/deploy-frontend.yml/badge.svg)](https://github.com/lilter96/portfolio/actions/workflows/deploy-frontend.yml)
[![License: MIT](https://img.shields.io/badge/license-MIT-green.svg)](./LICENSE)

A public monorepo demonstrating full-stack engineering — from infrastructure to user interface. Every
commit, architectural decision, and line of code is crafted to be read, critiqued, and learned from.

**Author:** Terentiy Gatsukov — Senior .NET Backend Developer, iGaming specialist
**Live:** [lilter96.github.io/portfolio](https://lilter96.github.io/portfolio/) &nbsp;|&nbsp; **License:** [MIT](./LICENSE)

---

## Structure

```
/
├── backend/     # .NET 10 — Minimal APIs, EF Core, PostgreSQL, Redis
│   ├── src/Api/          # Endpoints, middleware, host config
│   ├── src/Application/  # DTOs, mapping, use-case interfaces
│   ├── src/Domain/       # Entities, value objects, domain interfaces
│   └── src/Infrastructure/  # EF Core, Redis, health checks, seeding
├── frontend/    # React 19 + TypeScript + Vite
├── infra/       # docker-compose (infrastructure-only)
├── docs/        # Architecture decisions, runbooks, API specs
│   └── adr/     # Architecture Decision Records
└── .github/     # CI/CD workflows, PR templates
```

## Tech Stack

| Layer      | Choices                                              |
| ---------- | ---------------------------------------------------- |
| Runtime    | .NET 10.0.300 (LTS), C# latest                       |
| Backend    | ASP.NET Core Minimal APIs, endpoint groups, typed results |
| ORM        | EF Core 10.0.0, Npgsql PostgreSQL 10.0.0             |
| Validation | FluentValidation 12.0.0                               |
| Logging    | Serilog.AspNetCore 9.0.0 + Console sink 6.0.0        |
| API docs   | Scalar.AspNetCore 2.1.0, Asp.Versioning 8.1.0        |
| Data       | PostgreSQL 17, Redis 7                                |
| Cache      | Microsoft.Extensions.Caching.StackExchangeRedis 10.0.0 |
| Testing    | xunit 2.9.3, Vitest 4.x, Testing Library, Testcontainers |
| Frontend   | React 19 + TypeScript (strict) + Vite, TanStack Query v5 |
| Infra      | Docker (multi-stage) + docker-compose, GitHub Actions |
| CI/CD      | GitHub Actions                                        |

_See [ADR-0001](./docs/adr/0001-architecture-and-stack.md) for the rationale behind these choices._

## Getting Started

```bash
# Prerequisites: Docker
git clone https://github.com/lilter96/portfolio.git
cd portfolio

# Start everything (API + frontend + PostgreSQL + Redis)
docker compose up -d                # → http://localhost:3000

# Or run services individually:
cd backend && dotnet run --project src/Api   # → http://localhost:5121
cd frontend && npm ci && npm run dev         # → http://localhost:3000
```

See the [Makefile](./Makefile) for common tasks: `make up`, `make test`, `make logs`, `make help`.

## CI/CD

| Workflow | Trigger | What it does |
|----------|---------|-------------|
| **Backend CI** | push/PR on `backend/**` | Restore → build (warnings as errors) → unit tests + coverage → lint |
| **Frontend CI** | push/PR on `frontend/**` | npm ci → tsc + vite build → ESLint → Vitest + coverage |
| **Docker Build** | push/PR, path filters | Builds both Docker images via Buildx; pushes to GHCR on main |
| **Deploy Frontend** | push on main | Builds production bundle → deploys to GitHub Pages |
| **Deploy API** | push on main | Builds + pushes API container to GHCR |

Secrets are managed via [GitHub Actions secrets](https://docs.github.com/en/actions/security-for-github-actions/security-guides/using-secrets-in-github-actions). No secrets are committed to the repository.

## Guiding Principles

- **Code as craft.** Every PR is a portfolio piece — readable, tested, and documented.
- **Show, don't tell.** The slot demo proves iGaming domain expertise with real math and a real renderer.
- **Decisions as artifacts.** ADRs explain the _why_, not just the _what_.
- **Progressive disclosure.** The monorepo scaffolds complexity: simple at the root, depth in the leaves.
- **Real-world ready.** Observability, security, and operational concerns are first-class, not afterthoughts.

## Conventional Commits

This repo follows [Conventional Commits](https://www.conventionalcommits.org/). Commit messages are
structured as:

```
type(scope): description

feat(backend): add user registration endpoint
fix(frontend): correct nav focus trap on mobile
docs(adr): record database selection decision
```

Run `npx commitlint --from HEAD~1` to validate.
