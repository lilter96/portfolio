# Portfolio

A public monorepo demonstrating full-stack engineering — from infrastructure to user interface. Every
commit, architectural decision, and line of code is crafted to be read, critiqued, and learned from.

**Live:** _coming soon_ &nbsp;|&nbsp; **License:** [MIT](./LICENSE)

---

## Structure

```
/
├── backend/     # API services, domain logic, data access
├── frontend/    # Web application, design system, client code
├── infra/       # Infrastructure-as-code, provisioning, orchestration
├── docs/        # Architecture decisions, runbooks, API specs
│   └── adr/     # Architecture Decision Records
└── .github/     # CI/CD workflows, PR templates
```

## Tech Stack

| Layer      | Choices                                |
| ---------- | -------------------------------------- |
| Frontend   | TypeScript, React, Next.js, Tailwind   |
| Backend    | Go, REST + gRPC                        |
| Data       | PostgreSQL, Redis                      |
| Infra      | Terraform, Docker, (cloud TBD)         |
| CI/CD      | GitHub Actions                         |

_See [ADR-0001](./docs/adr/0001-architecture-and-stack.md) for the rationale behind these choices._

## Getting Started

```bash
# Prerequisites: Go 1.22+, Node.js 20+, Docker, Terraform

# Clone
git clone https://github.com/lilter96/portfolio.git
cd portfolio

# Backend
cd backend
go run ./cmd/server

# Frontend (separate terminal)
cd frontend
pnpm install
pnpm dev

# Infra (dry-run)
cd infra
terraform plan
```

## Guiding Principles

- **Code as craft.** Every PR is a portfolio piece — readable, tested, and documented.
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
