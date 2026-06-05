# Portfolio

A public monorepo demonstrating full-stack engineering — from infrastructure to user interface. Every
commit, architectural decision, and line of code is crafted to be read, critiqued, and learned from.

**Author:** Terentiy Gatsukov — Senior .NET Backend Developer, iGaming specialist
**Live:** _coming soon_ &nbsp;|&nbsp; **License:** [MIT](./LICENSE)

---

## Structure

```
/
├── backend/     # .NET 10 — Minimal APIs, EF Core, PostgreSQL, Redis
│   ├── src/Api/          # Endpoints, middleware, host config
│   ├── src/Application/  # DTOs, mapping, use-case interfaces
│   ├── src/Domain/       # Entities, value objects, domain interfaces
│   └── src/Infrastructure/  # EF Core, Redis, health checks, seeding
├── frontend/    # React 19 + TypeScript + Vite (planned)
├── infra/       # docker-compose, CI/CD
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
| Testing    | xunit 2.9.3, Microsoft.NET.Test.Sdk 17.14.1          |
| Frontend   | React 19 + TypeScript (strict) + Vite (planned)       |
| Infra      | Docker (multi-stage) + docker-compose, GitHub Actions |
| CI/CD      | GitHub Actions                                        |

_See [ADR-0001](./docs/adr/0001-architecture-and-stack.md) for the rationale behind these choices._

## Getting Started

```bash
# Prerequisites: .NET SDK 10.0.x, Node.js 22+, Docker

# Clone
git clone https://github.com/lilter96/portfolio.git
cd portfolio

# Start infrastructure (PostgreSQL + Redis)
docker compose -f infra/docker-compose.yml up -d

# Backend
cd backend
dotnet run --project src/Api         # → http://localhost:5121
# API docs at http://localhost:5121/scalar/v1

# Frontend (separate terminal, planned)
cd frontend
pnpm install && pnpm dev            # → http://localhost:3000
```

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
